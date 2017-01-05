
using InternationalCookies.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InternationalCookies.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;
        private IHelperService _helperService;

        public HomeController(ILogger<HomeController> logger, IHelperService helperService)
        {
            _logger = logger;
            _helperService = helperService;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Barries index is hit");
            return View();
        }

        public IActionResult About()
        {      
            return View();
        }

        public IActionResult Error()
        {          
            return View();
        }

        public IActionResult ClearCache()
        {
                _helperService.ClearCache(User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}
