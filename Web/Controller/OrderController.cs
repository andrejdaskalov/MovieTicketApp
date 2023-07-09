using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller
{
    public class OrderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET
        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }
    }
}