using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog.Context;

namespace LinkDev.Talabat.APIs.Middlewares
{
    public class ActionNameLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ActionNameLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Extract the action name from the HttpContext
            var endPoint = context.GetEndpoint();
            var actionName =  endPoint?.Metadata.GetMetadata<ControllerActionDescriptor>()?.ActionName;

            // Push the action name into the log context
            if (!string.IsNullOrEmpty(actionName))
            {
                using (LogContext.PushProperty("ActionName", actionName))
                {
                    await _next(context);// Continue to the next middleware
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
