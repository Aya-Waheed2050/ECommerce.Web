using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions.NotFoundExceptions;
using Domain.Models.BasketModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObject.OrderDtos;


namespace Service.Implementation
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork)
       : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest Request, string Email)
        {
            // mapping
            var Address = _mapper.Map<OrderAddress>(Request.ShipToAddress);

            // GetBasket 
            var Basket = await _basketRepository.GetAsync(Request.BasketId)
            ?? throw new BasketNotFoundException(Request.BasketId);

            ArgumentException.ThrowIfNullOrEmpty(Basket.PaymentIntentId);
            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var OrderSpec = new OrderWithPaymentIntentIdSpecifications(Basket.PaymentIntentId);
            var ExistingOrder = await OrderRepo.GetByIdAsync(OrderSpec);
            if (ExistingOrder is not null)
            {
                OrderRepo.Delete(ExistingOrder);
                await _unitOfWork.SaveChangesAsync();
            }

            // Create OrderItemList
            var orderItems = new List<OrderItem>();
            foreach (var item in Basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }
         
            // Get DeliveryMethod
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                                .GetByIdAsync(Request.DeliveryMethodId)
                                ?? throw new DeliveryMethodNotFoundException(Request.DeliveryMethodId);

            // Calculate SubTotal
            var SubTotal = orderItems.Sum(OI => OI.Quantity * OI.Price);

            var order = new Order(Email,Address,orderItems,SubTotal,DeliveryMethod,Basket.PaymentIntentId);


            await OrderRepo.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderResult>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {

            var order = await _unitOfWork.GetRepository<Order, Guid>()
                 .GetByIdAsync(new OrderWithIncludesSpecifications(id)) ?? throw new OrderNotFoundException(id);
            return _mapper.Map<Order, OrderResult>(order);
        }

        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string Email)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>()
               .GetAllAsync(new OrderWithIncludesSpecifications(Email));
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResult>>(orders);
        }


        #region Helper Method
        private OrderItem CreateOrderItem(BasketItem item, Product product)
         => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), product.Price, item.Quantity);

        #endregion


    }
}
