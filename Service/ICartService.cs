using System.Collections.Generic;
using Domain;

namespace Service
{
    public interface ICartService
    {
        public Cart GetCart(string username);
        public void AddToCart(string username, MovieTicket ticket, int quantity);
        public void DeleteFromCart(string username, MovieTicket ticket);
        public void DeleteAllFromCart(string username);
        public void ChangeQuantity(string username, MovieTicket ticket, int quantity);

        public IEnumerable<OrderItem> GetCartItems(string username);
    }
}