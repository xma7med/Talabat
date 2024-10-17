using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Identity
{
	public class Address
	{
		public int Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Street { get; set; }
		public required string City { get; set; }
		public required string Country { get; set; }
        public required string UserId { get; set; } //string bec the appuseridentyty TKey is string 

		/// Any Relation by default mapped 1 to many 
		/// Addede FK in the Required Side  
		/// Must Add it And make Data Annotation or fluent API to make it unique (No Duplicate )

		public virtual required ApplicationUser User { get; set; } // Mandatory 

    }
}
