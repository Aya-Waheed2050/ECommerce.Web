namespace Domain.Contracts
{
    public interface IDataSeeding
    {
        Task DataSeedAsync();
        Task IdentityDataSeedAsync();
    }
}
