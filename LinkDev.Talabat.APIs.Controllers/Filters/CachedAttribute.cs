using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Filters
{
    internal class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveInSecond;

        public CachedAttribute(int TimeToLiveInSecond )
        {
            timeToLiveInSecond = TimeToLiveInSecond;
        }
        //context : the context  of the action it self ||  next : refer to the next Action filter or action it self,
        //must be the end point so have to be the last action fillter not matter before or after attributes
        public async  Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // I will ask cashed service thrugh scoped way within request have all the services 
            /// NOTE : i didnt ask in ons bec if u didi , u will  need to pass the obj within any cash filtter [cashFiltter(obj)]
            var responseCachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCasheService>();

            var cashkey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var response = await responseCachedService.GetcachedResponseAsync(cashkey);

            if(!string.IsNullOrEmpty(response))// Response is already cached 
            {
                var result = new ContentResult
                {
                    Content = response, 
                    ContentType="application/json",
                    StatusCode = 200
                };

                context.Result = result;  
                return;
            }

           var executedEndpoint =  await next.Invoke();// Execute the Endpoint -- Thats why the cache action filter must be the last one   

            if(executedEndpoint.Result is OkObjectResult okObjectResult && okObjectResult.Value is not null)
            {
                await responseCachedService.CacheResponseAsync(cashkey, okObjectResult.Value,TimeSpan.FromSeconds(timeToLiveInSecond));
            }

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            // i neeed the key to be meaning full 

            // Example: {{url}}/api/products?pageIndex=1&pageSize=5&sort=name
            // user may make like this =>  {{url}}/api/products?pageIndex=1&sort=name&pageSize=5

            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path); // e.g., "api/products"

            // Example query parameters:
            // pageIndex = 1
            // pageSize  = 5
            // sort      = name

            foreach (var (key, value) in request.Query.OrderBy(x=>x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");

                 // Example result of keyBuilder:
                 // Key = api/products|pageIndex-1
                 // Key = api/products|pageIndex-1|pageSize-5
                 // Key = api/products|pageIndex-1|pageSize-5|sort-name

            }

            return keyBuilder.ToString();   
        }
    }
}
