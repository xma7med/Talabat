﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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


		
	}
}
