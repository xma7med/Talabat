using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{

	// This Controller Just To Seee The Default Responce For Ech Error 
	internal class BuggyController: ApiControllerBase
	{
		[HttpGet("notfound")] // GET: /api/buggy/notfound
		public IActionResult GetNotFoundRequest()
		{
			return NotFound(); // 404 Not Found
		}

		[HttpGet("servererror")] // GET: /api/buggy/servererror
		public IActionResult GetServerError()
		{
			throw new Exception(); // 500 Internal Server Error
		}

		[HttpGet("badrequest")] // GET: /api/buggy/badrequest
		public IActionResult GetBadRequest()
		{
			return BadRequest(new {StatusCode = 400 , Massage ="Bad Request" }); // 400 Bad Request
		}

		// Special Type Of BadRequest
		[HttpGet("badrequest/{id}")] // /api/buggy/badrequest/five     (if i send string id ) Will not Enter the End Point 
		public IActionResult GetValidationError(int id) // ==> 400 - // will not Enter or Ecute this if it is invalide state 
		{
			
			return Ok();
		}

		[HttpGet("unauthorized")] // GET: /api/buggy/unauthorized
		public IActionResult GetUnauthorizedError()
		{
			return Unauthorized(new { StatusCode = 401, Message = "Unauthorized" }); // 401
		}

		[HttpGet("forbidden")] // GET: /api/buggy/forbidden
		public IActionResult GetForbiddenRequest()
		{
			return Forbid(); // 403
		}


		[Authorize]
		[HttpGet("authorized")] // GET: /api/buggy/authorized
		public IActionResult GetAuthorizedRequest()
		{
			return Ok();
		}



	}
}
