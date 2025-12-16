using Domain.Models.OrderModule;

namespace Service.Specifications
{
    internal class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order, Guid>
    {

        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId)
            : base(o => o.PaymentIntentId == PaymentIntentId)
        {
        }
    }
}
