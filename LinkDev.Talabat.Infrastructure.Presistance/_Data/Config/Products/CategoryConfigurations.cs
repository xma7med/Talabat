using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Presistance.Data.Config.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Presistance.Data.Config.Products
{
	internal class CategoryConfigurations :BaseAuditableEntityConfigurations<ProductCategory, int>
	{
		public override void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			base.Configure(builder);


			builder.Property(C => C.Name)
				.IsRequired();
		}
	}
}
