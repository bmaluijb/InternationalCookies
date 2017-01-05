using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InternationalCookies.Data;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using InternationalCookies.Data.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System;

namespace InternationalCookies.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private Guid _storeId;
        private IOrderService _orderService;    
        private IDistributedCache _cache;

        public OrderController(IOrderService orderService, IDistributedCache cache)
        {
            _orderService = orderService;      
            _cache = cache;            
        }

        public IActionResult Index()
        {
            List<Order> orders = new List<Order>();

            if (Guid.TryParse(_cache.GetString(User.Identity.Name), out _storeId))
            {
                 orders = _orderService.GetAllOrdersByStore(_storeId);
            }

            return View(orders);
        }


        public IActionResult Detail(int id)
        {
            Order order = new Order();
            
            if (Guid.TryParse(_cache.GetString(User.Identity.Name), out _storeId))
            {
                order = _orderService.GetOrderById(id, _storeId);
            }

            return View(order);
        }

        public IActionResult CancelOrder(int id)
        {
            if (Guid.TryParse(_cache.GetString(User.Identity.Name), out _storeId))
            {
                _orderService.CancelOrder(id, _storeId);
            }

            return RedirectToAction("Index");
        }

        public IActionResult PlaceOrder(int id)
        {
            if (Guid.TryParse(_cache.GetString(User.Identity.Name), out _storeId))
            {
                _orderService.PlaceOrder(id, _storeId);
            }

            return RedirectToAction("Index");
        }

        public IActionResult AddCookieToOrderLine(int cookieId)
        {
            if (Guid.TryParse(_cache.GetString(User.Identity.Name), out _storeId))
            {

                _orderService.AddCookieToOrder(cookieId, _storeId);
            }

            return RedirectToAction("Index", "Order");
        }
    }
}
