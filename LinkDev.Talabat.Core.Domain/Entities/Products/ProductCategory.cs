﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Products
{
	public class ProductCategory : BaseAuditableEntity<int>
	{
        public required string  Name { get; set; }
    }
}
