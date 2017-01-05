using InternationalCookies.Data.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Services
{
    public class StoreService : IStoreService
    {
        private CookieContext _context;

        public StoreService(CookieContext context)
        {
            _context = context;

        }

        public Store GetStoreById(Guid storeId)
        {
            var store =_context.Stores.Where(s => s.Id == storeId).FirstOrDefault();

            return store;
        }    
        
        public Store GetDefaultStore()
        {
            return _context.Stores.FirstOrDefault();
        }    
    }
}
