using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Presistance.Common;
using System.Reflection;

namespace LinkDev.Talabat.Infrastructure.Presistance.Data
{
	public class StoreDbContext: DbContext
	{
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base (options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(/*StoreContext*/AssemblyInformation).Assembly); // You Cant no more Bec u have diff Configs for diff DB in same Project 
            /// First Way 
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
            //    type => type.Namespace!.Contains ( "LinkDev.Talabat.Infrastructure.Presistance.Data.Config"));

            /// Second Way 
             modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
               type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreDbContext));


        }


        ///public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        ///{
        ///	foreach (var entry in this.ChangeTracker.Entries<BaseAuditableEntity<int>>()
        ///		.Where(entity =>entity.State is EntityState.Added or EntityState.Modified))
        ///	{ 
        ///		if (entry.State is EntityState.Added)
        ///		{
        ///			entry.Entity.CreatedBy = "";
        ///			entry.Entity.CreatedOn = DateTime.UtcNow;	
        ///		}
        ///		entry.Entity.LastModifiedBy = "";
        ///		entry.Entity.LastModifiedOn = DateTime.UtcNow;	
        ///	}


        ///	return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        ///}
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
