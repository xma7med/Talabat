﻿using System.Text.Json;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Errors
{

	// for Handle Server Error = Exception
	public class ApiExceptionResponse : ApiResponse
	{
        public string ? Details { get; set; }


        public ApiExceptionResponse(int statusCode , string? message = null, string ? details = null  )
            :base(statusCode , message)
        {
            Details= details;
        }

        public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });


    }
}
