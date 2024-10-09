using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction
{
	public interface ILoggedInUserService
	{
        // Signature for one property 
        //  Auto-Implemented Property - shorthand

        public string? UserId { get;  }
    }
}
