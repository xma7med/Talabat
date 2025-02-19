using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Infrastructure
{
    public interface IResponseCasheService
    {
        // To cache the response   In Redis ( key value pair ) [ value : object bec i dont know what the type ]  & the time     
        Task CacheResponseAsync(string Key , object response , TimeSpan timeToLive);

        // string : The response is json , i will recive it as string || ? : maybe the cashed is expire ( No Cahed Data )
        Task<string?> GetcachedResponseAsync(string key);

    }
}
