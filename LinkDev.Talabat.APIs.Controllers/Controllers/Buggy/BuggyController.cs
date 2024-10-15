using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{

	// This Controller Just To Seee The Default Responce For Ech Error 
	public class BuggyController: ApiControllerBase
	{
		[HttpGet("notfound")] // GET: /api/buggy/notfound
		public IActionResult GetNotFoundError()
		{
			/// first  Default
			//return NotFound(); // 404 Not Found
			///second Way Anonymous obj Then  Enhance And Make The obj of standared response of ( NotFound , BadRequest , Unauthorized)
			//return NotFound(new { StatusCode = 400, Massage = "Bad Request" });
			return NotFound(new ApiResponse(404));
			/// Third way throw exception 
			//throw new NotFoundException();
		}

		[HttpGet("badrequest")] // GET: /api/buggy/badrequest
		public IActionResult GetBadRequest()
		{
			//return BadRequest(new {StatusCode = 400 , Massage ="Bad Request" }); // 400 Bad Request
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("unauthorized")] // GET: /api/buggy/unauthorized
		public IActionResult GetUnauthorizedError()
		{
			return Unauthorized(new ApiResponse(404)/*new { StatusCode = 401, Message = "Unauthorized" }*/); // 401
		}

		//**********************************************************************************************
		//**********************************************************************************************

		// 2 way --> 1 - handle exception may be thrown in every end piont or
		//           2- make middleWare th handle exception in genaric way to the whole app and if any EndOint have diff handling for exception we handle it 
		[HttpGet("servererror")] // GET: /api/buggy/servererror
		public IActionResult GetServerError()
		{
			/// Not recommended way 
			/// try
			///{
			///   throw new Exception(); // 500 Internal Server Error
			///}
			///catch (Exception ex)
			///{
			///	var response = new ApiResponse(500);
			///
			///	 Response.WriteAsync(JsonSerializer.Serialize(response));
			///
			///	return StatusCode(500);
			///}

			throw new Exception(); // 500 Internal Server Error


		}

		/// Handle it by Configure Faculty That generate the response of validation 
		// Special Type Of BadRequest
		[HttpGet("badrequest/{id}")] // /api/buggy/badrequest/five     (if i send string id ) Will not Enter the End Point 
		public IActionResult GetValidationError(int id) // ==> 400 - // will not Enter or Ecute this if it is invalide state 
		{

			//return BadRequest();
			/// (Not Recommanded )First way Disable the factory that generate the response to validation Errors
			//if (!ModelState.IsValid)
			//{
			//	var errors = ModelState.Where(P => P.Value.Errors.Count > 0)
			//							.SelectMany(P => P.Value.Errors) //SelectMany ف لازم اعمل  object  -حيث ان كل بروب عنها محموعه من الايرورز 
			//						    .Select(E => E.ErrorMessage);
			//	return BadRequest(new ApiValidationErrorResponse()
			//	{
			//		Errors = errors
			//	});
			//}

			return Ok();
			
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
