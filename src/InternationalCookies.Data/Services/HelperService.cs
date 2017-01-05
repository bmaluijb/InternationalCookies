using InternationalCookies.Data.Interfaces;
using InternationalCookies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Services
{
    public class HelperService : IHelperService
    {

        private IDistributedCache _cache;


        public HelperService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void ClearCache(string userName)
        {
            string storeId = _cache.GetString(userName);
            _cache.Remove(userName);
            _cache.Remove("connectionString" + storeId);   

        }
    }
}
