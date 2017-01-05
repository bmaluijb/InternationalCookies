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

    public class OrderController : Controller
    {
        private IOrderService _orderService;    
        private IDistributedCache _cache;
        private Guid _storeId = new Guid("2b76901f-ca39-417d-bfc3-498d0bb56e20");


        public OrderController(IOrderService orderService, IDistributedCache cache)
        {
            _orderService = orderService;      
            _cache = cache;            
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrdersByStore(_storeId);            

            return View(orders);
        }


        public IActionResult Detail(int id)
        {            
             var order = _orderService.GetOrderById(id, _storeId);           

            return View(order);
        }

        public IActionResult CancelOrder(int id)
        {
            _orderService.CancelOrder(id, _storeId);            

            return RedirectToAction("Index");
        }

        public IActionResult PlaceOrder(int id)
        {
            _orderService.PlaceOrder(id, _storeId);            

            return RedirectToAction("Index");
        }

        public IActionResult AddCookieToOrderLine(int cookieId)
        {
            _orderService.AddCookieToOrder(cookieId, _storeId);            

            return RedirectToAction("Index", "Order");
        }
    }
}
