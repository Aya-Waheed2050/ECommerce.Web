using System.Text.Json;
using Domain.Contracts;
using ServiceAbstraction;

namespace Service.Implementation
{
    class CacheService(ICacheRepository _cashRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string cashKey)
        => await _cashRepository.GetAsync(cashKey);

        public async Task SetAsync(string cashKey, object cashValue, TimeSpan timeToLive)
        => await _cashRepository.SetAsync(cashKey, cashValue, timeToLive);
        
    }
}
