﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class ProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductToReturnDto, string>
	{
		public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
				return $"{configuration["Urls:ApisBaseUrl"]}/{source.PictureUrl}";
			return string.Empty;
		}
	}
}
