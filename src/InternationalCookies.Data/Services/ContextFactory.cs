using InternationalCookies.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Services
{
    public class ContextFactory : IContextFactory
    {
        private IRoutingService _routingService;
        public ContextFactory(IRoutingService routingService)
        {
            _routingService = routingService;
        }

        public CookieContext Context(Guid storeId)
        {
            return _routingService.GetRoutedContext(storeId);
        }
    }
}
