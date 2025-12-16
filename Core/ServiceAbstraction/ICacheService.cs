namespace ServiceAbstraction
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string cashKey);
        Task SetAsync(string cashKey ,object cashValue , TimeSpan timeToLive);

    }
}
