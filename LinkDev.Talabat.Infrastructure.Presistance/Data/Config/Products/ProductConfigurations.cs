using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Presistance.Data.Config.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Presistance.Data.Config.Products
{
	public class ProductConfigurations : BaseAuditableEntityConfigurations<Product,int>      /*IEntityTypeConfiguration<Product>*/
	{
		public override void Configure(EntityTypeBuilder<Product> builder)
		{
			/// in Base Configs 
			/// builder.Property(E => E.Id)
			/// 	.UseIdentityColumn(1, 1);
			/// 
			/// 
			/// builder.Property(E => E.CreatedBy)
			/// 	.IsRequired();
			/// 
			/// builder.Property(E => E.CreatedOn)
			/// 	.IsRequired();
			/// //.HasDefaultValue("GETUTCDATE()"); // i will use inspector 
			/// 
			/// 
			/// builder.Property(E => E.LastModifiedBy)
			/// 	.IsRequired();
			/// 
			/// builder.Property(E => E.LastModifiedOn)
			/// 	.IsRequired();
			/// //.HasDefaultValue("GETUTCDATE()"); // i will use inspector 
			/// 

			base.Configure(builder);

			builder.Property(P => P.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(P => P.Description)
				.IsRequired();

			builder.Property(P => P.Price)
				.HasColumnType("decimal(9, 2)");


			builder.HasOne(P=>P.Brand)
				.WithMany()
				.HasForeignKey(P=>P.BrandId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(P => P.Category)
				.WithMany()
				.HasForeignKey(P => P.CategoryId)
				.OnDelete(DeleteBehavior.SetNull);


		}
	}
}
