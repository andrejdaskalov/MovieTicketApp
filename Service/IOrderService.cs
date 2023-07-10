using System;
using System.Collections.Generic;
using Domain;

namespace Service
{
    public interface IOrderService
    {
        public Order CreateOrder(Cart cart);
        public IEnumerable<Order> GetAllOrders();
        public Order GetOrderById(Guid id);
    }
}