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
        public required IEnumerable<ValidationError> Errors { get; set; }

        public ApiValidationErrorResponse(string ? message = null )
            :base (400 , message)
        {
            
        }

		public class ValidationError // Default Accsess Modifire private 
		{
			public required string Field { get; set; }
			public required IEnumerable<string> Errors { get; set; }
		}

	}
}
