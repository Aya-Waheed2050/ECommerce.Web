using System.Text.Json;
using Domain.Contracts;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class CacheRepository (IConnectionMultiplexer _connection): ICacheRepository
    {

        private readonly IDatabase _database = _connection.GetDatabase();

        public async Task<string?> GetAsync(string CashKey)
        {
            RedisValue cashValue = await _database.StringGetAsync(CashKey);
            return cashValue.IsNullOrEmpty ? default : cashValue;
        }

        public async Task SetAsync(string CashKey, object CashValue, TimeSpan TimeToLive)
        {
           var serializeObj = JsonSerializer.Serialize(CashValue); 
           await _database.StringSetAsync(CashKey, serializeObj, TimeToLive);
        }
    }
}
