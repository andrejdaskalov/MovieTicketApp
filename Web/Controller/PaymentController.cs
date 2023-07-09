using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Stripe;

namespace Web.Controller
{
    [Authorize]
    public class PaymentController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public PaymentController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        const string endpointSecret = "whsec_2596e314e0c85979f83b3cd60d54a3a8a42a4c7ba445ccdc0350cba034f8479a";
        
        
        public IActionResult Processing(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.Name);
            
            var cart = _cartService.GetCart(userId);
            // var order = this._shoppingCartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source =stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(cart.TotalPrice) * 100),
                Description = "EShop Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if(charge.Status == "succeeded")
            {
                // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = _orderService.CreateOrder(cart);
            }

            return RedirectToAction("ViewCart", "Cart");
        }
    }
    
}