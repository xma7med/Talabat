using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Presistance.Data.Config.Base
{
	internal class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
		where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(E => E.Id)
		           .ValueGeneratedOnAdd(); // Bec i cant use Identity not all entity has int PK
								// This Meth(fluentAPIs ) ==> Generate if PK numeric 1,1 if Guid will generate Guid on Add (Insert)

		}
	} 
}
