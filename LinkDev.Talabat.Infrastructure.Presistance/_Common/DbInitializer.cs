using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;

namespace LinkDev.Talabat.Infrastructure.Presistance.Common
{
    public abstract class DbInitializer(DbContext _dbContext) : IDbIntializer
    {
        // untill now i dont need it virtual 
        public /*virtual*/ async Task InitializeAsync() // Update- Database
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync(); 
        }
        // abstract member must be in abstract container 
        public abstract Task SeedAsync(); // i will not implement it here 
    }
}
