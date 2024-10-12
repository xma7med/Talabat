﻿using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Product_Specs
{
	public class ProductWithFilterationForCountSpecifications : BaseSpecifications<Product, int>
	{
        public ProductWithFilterationForCountSpecifications(int? brandId, int? categoryId)
            : base(

				  P =>
				   ((!brandId.HasValue) || P.BrandId == brandId.Value)
					&&
				   ((!categoryId.HasValue) || P.CategoryId == categoryId.Value)
				  ) 
        {
            
        }
    }
}
