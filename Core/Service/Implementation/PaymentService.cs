using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using Domain.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketDtos;
using Stripe;
using Product = Domain.Models.ProductModule.Product;
using Order = Domain.Models.OrderModule.Order;
using Service.Specifications;
using Domain.Exceptions.NotFoundExceptions;
namespace Service.Implementation
{
    public class PaymentService(IConfiguration _configuration,
        IBasketRepository _basketRepository,
        IUnitOfWork _unitOfWork, IMapper _mapper)
        : IPaymentService
    {

        #region CreateOrUpdatePaymentIntentAsync + HelperMethod

        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {

            // Configure Stripe (Install Package Stripe.Net)
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];

            // Get Basket By BasketID
            CustomerBasket? basket = await GetBasketAsync(basketId);

            // Get Amount - Get Product + Delivery Method Cost
            await ValidateBasketAsync(basket);

            var basketAmount = CalculateTotalAsync(basket);

            // Create Payment Intent [Create - Update]
            await CreationOrUpdatePaymentIntentAsync(basket, basketAmount);

            //Save
            await _basketRepository.CreateOrUpdateAsync(basket);
            return _mapper.Map<BasketDto>(basket);
        }


        private async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            return await _basketRepository.GetAsync(basketId) ??
             throw new BasketNotFoundException(basketId);
        }
        private async Task ValidateBasketAsync(CustomerBasket basket)
        {
            foreach (var item in basket.Items)
            {
                Product? product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                        ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }

            ArgumentNullException.ThrowIfNull(basket.DeliveryMethodId);
            DeliveryMethod? DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = DeliveryMethod.Price;
        }
        private long CalculateTotalAsync(CustomerBasket basket)
        {
            return (long)((basket.Items.Sum(item => item.Price * item.Quantity) + basket.ShippingPrice) * 100);
        }
        private async Task CreationOrUpdatePaymentIntentAsync(CustomerBasket basket, long amount)
        {
            var paymentService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId)) //Create
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent = await paymentService.CreateAsync(options);
                basket.ClientSecret = PaymentIntent.ClientSecret;
                basket.PaymentIntentId = PaymentIntent.Id;
            }
            else // Update
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = amount,
                };
                await paymentService.UpdateAsync(basket.PaymentIntentId, options);
            }
        }

        #endregion

        #region UpdatePaymentStatus + HelperMethod

        public async Task UpdatePaymentStatusAsync(string request, string stripeHeaders)
        {


            var endPointSecret = _configuration.GetSection("StripeSettings")["WebhookSecret"];

            //var stripeEvent = EventUtility.ParseEvent(request , throwOnApiVersionMismatch:false);
            var stripeEvent = EventUtility.ConstructEvent(request, stripeHeaders, endPointSecret , throwOnApiVersionMismatch:false);
            Console.WriteLine($"Stripe Event: {stripeEvent.Type}");

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            Console.WriteLine($"PaymentIntentId: {paymentIntent?.Id}");
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentStatusSucceeded(paymentIntent.Id);
                    break;
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentStatusFailed(paymentIntent.Id);
                    break;
                default:
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }

        }

        private async Task UpdatePaymentStatusFailed(string PaymentIntentId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo
                   .GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(PaymentIntentId));
            if (order is not null)
            {
                order.Status = OrderPaymentStatus.PaymentFailed;
                orderRepo.Update(order);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task UpdatePaymentStatusSucceeded(string PaymentIntentId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(PaymentIntentId));
            if (order is not null)
            {
                order.Status = OrderPaymentStatus.PaymentReceived;
                orderRepo.Update(order);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion

    }
}
