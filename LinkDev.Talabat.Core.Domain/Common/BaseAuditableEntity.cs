using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Common
{
	public class BaseAuditableEntity<TKey>:BaseEntity<TKey>
		where TKey : IEquatable<TKey>  // TKey : To make Id genaric Each Entity Specify their own type
																 // IEquatable<TKey> to enable Eqality for Genaric repo in GetById & Find need id to be eqautable 
	{


		public required string CreatedBy { get; set; } // 3 way will use inspector 

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

		public required string  LastModifiedBy { get; set; }
        public  DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;

	}
}
