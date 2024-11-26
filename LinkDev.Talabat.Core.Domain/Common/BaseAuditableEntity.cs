﻿namespace LinkDev.Talabat.Core.Domain.Common
{
	public abstract class BaseAuditableEntity<TKey>:BaseEntity<TKey>
		where TKey : IEquatable<TKey>  // TKey : To make Id genaric Each Entity Specify their own type
																 // IEquatable<TKey> to enable Eqality for Genaric repo in GetById & Find need id to be eqautable 
	{


		public string CreatedBy { get; set; } = null!; // 3 way will use inspector 

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

		public string  LastModifiedBy { get; set; } = null!;
        public  DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;

	}
}
