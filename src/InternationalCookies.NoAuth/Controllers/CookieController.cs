using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InternationalCookies.Data;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using InternationalCookies.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace InternationalCookies.Controllers
{

    public class CookieController : Controller
    {
        private ICookieService _cookieService;

        public CookieController(ICookieService cookieService)
        {    
            _cookieService = cookieService;          
        }

        public IActionResult Index()
        {
            return View(_cookieService.GetAllCookies());
        }

    }
}
