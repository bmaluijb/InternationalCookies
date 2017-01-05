using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InternationalCookies.Data.Interfaces;
using InternationalCookies.Data.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace InternationalCookies.Controllers
{
  
    [Route("api/[controller]")]
    public class RouteController : Controller
    {
        private IRoutingService _routingService;
        public RouteController(IRoutingService routingService)
        {
            _routingService = routingService;
        }
       

        [HttpGet]
        public async void Get()
        {
             _routingService.SyncStoresToDatabases();
        }
    }
}
