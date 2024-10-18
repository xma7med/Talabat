using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
