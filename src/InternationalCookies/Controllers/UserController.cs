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
    public class UserController : Controller
    {
        private IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }
       
        [HttpPost]
        public async Task<string> Post([FromBody]Register value, string storeId)
        {         
            return await _accountService.CreateNewUser(value, storeId);
        }
    }
}
