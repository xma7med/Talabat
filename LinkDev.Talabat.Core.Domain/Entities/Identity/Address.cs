using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Identity
{
	/// maybe u ask why i dont inherit the id from BaseEntity ? 
	// Bec i will seperate the database and make Db for security data  ( security ) 
	public class Address// the follow table التابع مش الاساسي 
	{
		public int Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Street { get; set; }
		public required string City { get; set; }
		public required string Country { get; set; }
		// what forbidden to have many address to user ? 1- add FK 2-Make unique constraints 
        public required string UserId { get; set; } //string bec the appuseridentyty TKey is string 

//Any Relation by default mapped 1 to many Untill u add constarints 
//Must Add Added FK from optional to the Required Side-> Untill now there are no constraints that forbid Duplication
//Add Unique Constraint to forbidden duplication by make Data Annotation or fluent API to make it unique (No Duplicate )

        public virtual required ApplicationUser User { get; set; } // Mandatory 

    }
}
