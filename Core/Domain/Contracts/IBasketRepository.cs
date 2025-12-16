using Domain.Models.BasketModule;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
         Task<CustomerBasket?> GetAsync(string Key);
         Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket basket , TimeSpan? timeToLive=null);
         Task<bool> DeleteAsync(string id);
    }
}
