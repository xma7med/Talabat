#nullable disable
namespace LinkDev.Talabat.Core.Domain.Common
{
	public abstract class BaseEntity<TKey>
		where TKey : IEquatable<TKey>  // TKey : To make Id genaric Each Entity Specify their own type

	{
		public  TKey Id { get; set; }

	}
}
