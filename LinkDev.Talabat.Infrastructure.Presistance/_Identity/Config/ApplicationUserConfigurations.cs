﻿using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Presistance._Identity.Config
{
	//  IEntityTypeConfiguration<TEntity>
	public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(U => U.DisplayName)
	               .HasColumnType("varchar")
	               .HasMaxLength(100)
	               .IsRequired();
	               
			builder.HasOne(U => U.Address)
				.WithOne(A => A.User)
				.HasForeignKey<Address>(A => A.UserId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
