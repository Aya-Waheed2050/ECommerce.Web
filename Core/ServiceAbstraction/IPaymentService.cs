using Shared.DataTransferObject.BasketDtos;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);

        Task UpdatePaymentStatusAsync(string request, string stripeHeaders);

    }
}
