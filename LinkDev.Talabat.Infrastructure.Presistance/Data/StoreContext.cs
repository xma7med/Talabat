using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Presistance.Data
{
	public class StoreContext: DbContext
	{
        public StoreContext(DbContextOptions<StoreContext> options):base (options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(/*StoreContext*/AssemblyInformation).Assembly);
		}

		public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
    }
}
