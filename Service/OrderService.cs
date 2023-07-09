using Domain;
using Repository;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Cart> _cartRepository;

        public OrderService(IRepository<Cart> cartRepository, IRepository<Order> orderRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        public Order CreateOrder(Cart cart)
        {
            var order = _orderRepository.Insert(new Order
            {
                Id = cart.Id,
                OrderItems = cart.CartItems,
                User = cart.User
            });
            _cartRepository.Delete(cart);
            return order;
        }
    }
}