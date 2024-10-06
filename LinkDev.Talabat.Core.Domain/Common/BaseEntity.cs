﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Common
{
	public class BaseEntity<TKey> where TKey : IEquatable<TKey>	
	{
		public required TKey Id { get; set; }


		public required string CreatedBy { get; set; } // 3 way will use inspector 

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

		public required string  LastModifiedBy { get; set; }
        public  DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;

	}
}
