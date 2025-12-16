using System.Text.Json;
using Domain.Contracts;
using Domain.Models.BasketModule;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();

        public async Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            string? JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id , JsonBasket , timeToLive ?? TimeSpan.FromDays(30));
            return (IsCreatedOrUpdated) ? await GetAsync(basket.Id) : null;
        }
 
        public async Task<CustomerBasket?> GetAsync(string Key)
        {
            RedisValue basket = await _database.StringGetAsync(Key);
            return (basket.IsNullOrEmpty) ? null :
                   JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }

        public async Task<bool> DeleteAsync(string id)
        => await _database.KeyDeleteAsync(id);

    }
}
