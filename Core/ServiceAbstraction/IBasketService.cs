using Shared.DataTransferObject.BasketDtos;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<BasketDto> GetAsync(string Key);
        Task<BasketDto> CreateOrUpdateAsync(BasketDto basket);
        Task<bool> DeleteAsync(string Key);
    }
}
