using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InternationalCookies.Models;
using Microsoft.Extensions.Options;

namespace InternationalCookies.ViewComponents
{
    public class RegionViewComponent : ViewComponent
    {
        private readonly AppSettings _appSettings;

        public RegionViewComponent(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string region = _appSettings.Region;

            ViewData.Add("region", region);

            return View();
        }
    }
}
