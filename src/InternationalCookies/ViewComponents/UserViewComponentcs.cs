using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InternationalCookies.Models;
using Microsoft.Extensions.Options;
using InternationalCookies.Data.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using InternationalCookies.Data;
using System.Security.Claims;

namespace InternationalCookies.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        private IAccountService _accountService;
        private IDistributedCache _cache;

        public UserViewComponent(IAccountService accountService, IDistributedCache cache)
        {
            _cache = cache;
            _accountService = accountService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(_cache.GetString(User.Identity.Name)) || _cache.GetString(User.Identity.Name) == "-1")
                {
                    ClaimsIdentity user = (ClaimsIdentity)User.Identity;
                    string userId = user.Claims.First(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

                    Guid storeId = await _accountService.GetStoreIdFromUser(userId);

                    _cache.SetString(User.Identity.Name, storeId.ToString());
                }
            }

            return View();
        }
    }
}
