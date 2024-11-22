namespace LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers
{
    public interface IDbIntializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
