using InternationalCookies.Data.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Services
{
    public class CookieService : ICookieService
    {
        private CookieContext _context;
        private IDistributedCache _cache;

        public CookieService(CookieContext context, IDistributedCache cache, IRoutingService routingService)
        {
            _context = context;
            _cache = cache;
        }

        public List<Cookie> GetAllCookies()
        {
            List<Cookie> cookies;

            //first, try to get cookies from cache
            var cachedCookies = _cache.GetString("cookies");
            if (!string.IsNullOrEmpty(cachedCookies))
            {
                //if they are there, deserialize them
                cookies = JsonConvert.DeserializeObject<List<Cookie>>(cachedCookies);
            }
            else
            {
                //if no cookies in are in cache yet, get them from the database
                cookies = _context.Cookies.ToList();

                //and then, put them in cache
                _cache.SetString("cookies", JsonConvert.SerializeObject(cookies));
            }

            return cookies;
        }

        
    }
}
