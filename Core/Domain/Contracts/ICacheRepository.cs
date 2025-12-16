namespace Domain.Contracts
{
    public interface ICacheRepository
    {

        Task<string?> GetAsync(string CashKey);
        Task SetAsync(string CashKey ,object CashValue , TimeSpan TimeToLive);
    }

}
