using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Identity
{
	public class ApplicationUser
	{
        public required string DisplayName { get; set; }
        public Address? Address  { get; set; } // Optional
    }
}
