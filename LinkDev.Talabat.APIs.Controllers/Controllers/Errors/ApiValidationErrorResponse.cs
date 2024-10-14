using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Errors
{
	// To use it to handle Validations By Disable 
	/*
            .ConfigureApiBehaviorOptions(options =>
										 {
											 options.SuppressModelStateInvalidFilter = true;	
										 }) 
     */


	public class ApiValidationErrorResponse : ApiResponse
	{
// The Error Collection for every parameter error   Key (Parameter)           Value(Parameter Error )
        public required IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse(string ? message = null )
            :base (400 , message)
        {
            
        }
    }
}
