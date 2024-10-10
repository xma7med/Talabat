﻿using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Presistance.Data.Config.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Presistance.Data.Config.Products
{
	internal class BrandConfigurations : BaseAuditableEntityConfigurations<ProductBrand,int>
	{

		public override void Configure(EntityTypeBuilder<ProductBrand> builder)
		{
			base.Configure(builder);

			builder.Property(B => B.Name)
				.IsRequired();
		}
	}
}
