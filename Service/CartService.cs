using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Identity;
using Repository;

namespace Service
{
    public class CartService : ICartService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;

        public CartService(UserManager<IdentityUser> userManager, IRepository<Cart> cartRepository,
            IRepository<OrderItem> orderItemRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _orderItemRepository = orderItemRepository;
        }

        public Cart GetCart(string username)
        {
            var user = _userManager.Users.First(u => u.UserName == username);

            var cart = _cartRepository.GetAll().FirstOrDefault(c => c.User.Equals(user));
            if (cart != null)
            {
                return cart;
            }
            else
            {
                var newCart = new Cart
                {
                    User = user,
                    CartItems = new System.Collections.Generic.List<OrderItem>()
                };
                var insertedCart = _cartRepository.Insert(newCart);
                return insertedCart;
            }
        }

        public IEnumerable<OrderItem> GetCartItems(string username)
        {
            var cart = GetCart(username);
            return cart.CartItems;
        }

        public void AddToCart(string username, MovieTicket ticket, int quantity)
        {
            var item = new OrderItem
            {
                MovieTicket = ticket,
                Quantity = quantity
            };
            var orderItem = _orderItemRepository.Insert(item);
            var cart = GetCart(username);
            cart.CartItems.Add(orderItem);
            // orderItem.Cart = cart;
            cart.TotalPrice += orderItem.MovieTicket.Price * orderItem.Quantity;
            _cartRepository.Update(cart);
        }

        public void DeleteFromCart(string username, MovieTicket ticket)
        {
            var cart = GetCart(username);
            var item = cart.CartItems.FirstOrDefault(i => i.MovieTicket.Equals(ticket));
            if (item != null)
            {
                cart.CartItems.Remove(item);
                _orderItemRepository.Delete(item);
                cart.TotalPrice -= item.MovieTicket.Price * item.Quantity;
                _cartRepository.Update(cart);
            }
        }

        public void DeleteAllFromCart(string username)
        {
            var cart = GetCart(username);
            foreach (var item in cart.CartItems)
            {
                _orderItemRepository.Delete(item);
            }
            cart.CartItems.Clear();
            _cartRepository.Update(cart);
        }

        public void ChangeQuantity(string username, MovieTicket ticket, int quantity)
        {
            var cart = GetCart(username);
            var item = cart.CartItems.FirstOrDefault(i => i.MovieTicket.Equals(ticket));
            if (item != null)
            {
                cart.TotalPrice -= item.MovieTicket.Price * item.Quantity;
                item.Quantity = quantity;
                cart.TotalPrice += item.MovieTicket.Price * item.Quantity;
                _cartRepository.Update(cart);
                _orderItemRepository.Update(item);
            }
        }
    }
}