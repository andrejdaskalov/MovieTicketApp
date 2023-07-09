using Domain;

namespace Service
{
    public interface IOrderService
    {
        public Order CreateOrder(Cart cart);
    }
}