using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Cach_Service
{
    public class ResponseCasheService (IConnectionMultiplexer redis): IResponseCasheService
    {
        private readonly IDatabase _database = redis.GetDatabase();
        public async Task CacheResponseAsync(string Key, object response, TimeSpan timeToLive)
        {
            if (response is null) return ;
            // Convert response to Json 

            var serilizedOption = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };    
            var serilizedResponse = JsonSerializer.Serialize(response , serilizedOption);

            await _database.StringSetAsync(Key, serilizedResponse, timeToLive); 
        }

        public async Task<string?> GetcachedResponseAsync(string key)
        {
            var response = await _database.StringGetAsync(key);
            if (response.IsNull) return null;

            return response;
        }
    }
}
