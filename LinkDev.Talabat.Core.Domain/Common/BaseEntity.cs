using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Common
{
	public class BaseEntity<TKey>
		where TKey : IEquatable<TKey>  // TKey : To make Id genaric Each Entity Specify their own type

	{
		public required TKey Id { get; set; }

	}
}
