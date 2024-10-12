﻿using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Common
{
	public class Pagination<T> 
	{
		

		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int Count { get; set; }
        public required IEnumerable<T> Data { get; set; }
		// required forbidden new to intialize so u must provide a value 

		public Pagination(int pageSize, int pageIndex/*, IEnumerable<T> data*/, int  count)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			//Data = data;	
			Count = count;
		}
	}
}
