using LinkDev.Talabat.Core.Application.Abstraction;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Services
{
	public class LoggedInUserService : ILoggedInUserService
	{
		private readonly IHttpContextAccessor? _httpContextAccessor;
        public string UserId { get; }

		public LoggedInUserService(IHttpContextAccessor? httpContextAccessor)
        {
			_httpContextAccessor = httpContextAccessor;
			UserId = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
			//HttpContext inside all req info even need to be authenticated or not 
			// user prop when the req need to authienticated this prop save the info of user 
			// token made of encryption Key and some claims (other input )
		}
	}
}
