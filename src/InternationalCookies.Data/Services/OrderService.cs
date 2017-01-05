using InternationalCookies.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Services
{
    public class OrderService : IOrderService
    {
        private IDistributedCache _cache;
        private IStoreService _storeService;
        private IContextFactory _contextFactory;

        public OrderService(IDistributedCache cache, IStoreService storeService, IContextFactory contextFactory)
        {
            _cache = cache;
            _storeService = storeService;
            _contextFactory = contextFactory;

        }

        public void AddCookieToOrder(int cookieId, Guid storeId)
        {
            CookieContext context = _contextFactory.Context(storeId);
            var orderString = _cache.GetString(storeId.ToString());
            Order storesOrder;

            if (!string.IsNullOrEmpty(orderString))
            {
                storesOrder = JsonConvert.DeserializeObject<Order>(orderString);

                bool orderLineExists = false;
                foreach (var lines in storesOrder.OrderLines)
                {
                    if (lines.Cookie.Id == cookieId)
                    {
                        lines.Quantity++;
                        orderLineExists = true;

                        storesOrder.Price += lines.Cookie.Price;
                    }
                }

                if (!orderLineExists)
                {
                    var cookie = context.Cookies.Where(c => c.Id == cookieId).FirstOrDefault();

                    storesOrder.OrderLines.Add(new OrderLine
                    {
                        Cookie = cookie,
                        Quantity = 1
                    });

                    storesOrder.Price += cookie.Price;
                }
            }
            else
            {
                storesOrder = new Order();
                storesOrder.Date = DateTimeOffset.Now;
                storesOrder.Store = _storeService.GetStoreById(storeId);
                storesOrder.Status = "New";

                var cookie = context.Cookies.Where(c => c.Id == cookieId).FirstOrDefault();

                storesOrder.OrderLines.Add(new OrderLine
                {
                    Cookie = cookie,
                    Quantity = 1
                });

                storesOrder.Price += cookie.Price;
            }

            _cache.SetString(storeId.ToString(), JsonConvert.SerializeObject(storesOrder));
        }

        public List<Order> GetAllOrdersByStore(Guid storeId)
        {
            //get orders from the database
            var orders = _contextFactory.Context(storeId).Orders
                            .Include(o => o.OrderLines)
                            .Include(o => o.Store)
                            .Where(s => s.Store.Id == storeId)
                            .ToList();

            //also, get the order from cache
            //this can be only one for this store, as it would be the new order
            //basically a shopping-cart
            var newOrder = _cache.GetString(storeId.ToString());
            if (!string.IsNullOrEmpty(newOrder))
            {
                orders.Add(JsonConvert.DeserializeObject<Order>(newOrder));
            }

            //sort the orders by date
            orders = orders.OrderBy(o => o.Date).ToList();

            return orders;

        }

        public Order GetOrderById(int orderId, Guid storeId)
        {
            Order order = null;

            if (orderId == 0)
            {//we know that it is a new order (one that lives in the cache)
                var newOrder = _cache.GetString(storeId.ToString());
                if (!string.IsNullOrEmpty(newOrder))
                {
                    order = JsonConvert.DeserializeObject<Order>(newOrder);
                }
            }
            else
            {
                //the order is in the database, so retrieve it from there
                order = _contextFactory.Context(storeId).Orders
                            .Include(o => o.OrderLines)
                                .ThenInclude(OrderLine => OrderLine.Cookie)
                            .Include(o => o.Store)
                            .Where(o => o.Id == orderId).FirstOrDefault();
            }

            return order;
        }

        public void CancelOrder(int orderId, Guid storeId)
        {
            CookieContext context = _contextFactory.Context(storeId);

            if (orderId == 0)
            {
                //cancel the order in cache
                _cache.Remove(storeId.ToString());
            }
            else
            {
                //the order is in the database, remove it
                var order = context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
                if (order != null)
                {
                    context.Remove(order);
                    context.SaveChanges();
                }
            }
        }

        public void PlaceOrder(int orderId, Guid storeId)
        {
            CookieContext context = _contextFactory.Context(storeId);
            
            if (orderId == 0) //just to check that this is the new order with id 0
            {
                var newOrder = _cache.GetString(storeId.ToString());
                if (!string.IsNullOrEmpty(newOrder))
                {
                    var order = JsonConvert.DeserializeObject<Order>(newOrder);

                    foreach (var line in order.OrderLines)
                    {
                        //have to get a reference to the actual cookie, not the attached one, otherwise EF will protest
                        line.Cookie = context.Cookies.Where(c => c.Id == line.Cookie.Id).FirstOrDefault();
                    }

                    order.Status = "Placed";

                    //have to get a reference to the actual store, not the attached one, otherwise EF will protest
                    order.Store = context.Stores.Where(s => s.Id == order.Store.Id).FirstOrDefault();

                    context.Orders.Add(order);

                    if (context.SaveChanges() > 0) //check for success
                    {
                        //if all went well, remove the item from cache
                        _cache.Remove(storeId.ToString());
                    }
                }
            }
        }
    }
}
