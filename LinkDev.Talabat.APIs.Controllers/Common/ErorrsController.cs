using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Common
{
	[ApiController]
	[Route("Errors/{Code}")]
	[ApiExplorerSettings(IgnoreApi =false )] // I dont need Documentaion(Swagger) for this Controller 
	public class ErorrsController : ControllerBase // 3shan Elroute Bta3o Mokhtalef showaya
	{
		[HttpGet]
		public IActionResult Error(int Code)
		{ 
		    if (Code  == (int)HttpStatusCode.NotFound)
			{
				var response = new ApiResponse((int)HttpStatusCode.NotFound, $"The requested endpoint {Request.Path}  not found.");
				return NotFound(response);
			}
			// and Continoue or use switch 

			return StatusCode(Code);	// AnyThing Else  Return the default 
		}

	}
}
