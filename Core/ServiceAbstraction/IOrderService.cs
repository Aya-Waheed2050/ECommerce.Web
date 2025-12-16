using Shared.DataTransferObject.OrderDtos;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderResult> CreateOrderAsync(OrderRequest orderDto , string Email);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();
        Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string Email);
        Task<OrderResult> GetOrderByIdAsync(Guid id);
    }
}
