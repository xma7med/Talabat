namespace LinkDev.Talabat.Core.Domain.Contracts.Persistence
{
	public interface IStoreContextIntializer
    {
        Task InitializeAsync();
        Task SeedAsync();

    }
}
