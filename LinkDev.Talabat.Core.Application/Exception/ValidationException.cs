namespace LinkDev.Talabat.Core.Application.Exception
{
	public class ValidationException : BadRequestException
	{
        // Errors 
        public IEnumerable<string> Errors { get; set; }
        public ValidationException(string? massage = "Bad Request ")
            :base(massage)
        {
            
        }

    }
}
