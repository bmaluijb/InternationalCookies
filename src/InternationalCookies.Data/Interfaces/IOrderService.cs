using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Interfaces
{
    public interface IOrderService
    {
        void AddCookieToOrder(int cookieId, Guid storeId);

        List<Order> GetAllOrdersByStore(Guid storeId);

        Order GetOrderById(int orderId, Guid storeId);

        void CancelOrder(int orderId, Guid storeId);

        void PlaceOrder(int orderId, Guid storeId);

    }
}
