﻿using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Presistance.Data.Config.Base
{
	internal class BaseAuditableEntityConfigurations<TEntity, TKey> : BaseEntityConfigurations<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
	{
		public override void  Configure(EntityTypeBuilder<TEntity> builder)
		{
			

			builder.Property(E => E.CreatedBy)
				.IsRequired();

			builder.Property(E => E.CreatedOn)
				.IsRequired();
			//.HasDefaultValue("GETUTCDATE()"); // i will use inspector 


			builder.Property(E => E.LastModifiedBy)
				.IsRequired();

			builder.Property(E => E.LastModifiedOn)
				.IsRequired();
			//.HasDefaultValue("GETUTCDATE()"); // i will use inspector 
		}
	}
}
