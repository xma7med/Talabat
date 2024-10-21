using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;

namespace LinkDev.Talabat.Infrastructure.Presistance.Common
{
    public abstract class DbInitializer(DbContext _dbContext) : IDbIntializer
    {
        public virtual async Task InitializeAsync() // Update- Database
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync(); // Update-DataBase
        }

        public abstract Task SeedAsync();
    }
}
