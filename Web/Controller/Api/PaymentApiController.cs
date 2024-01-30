using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Service;
using Stripe;
using Stripe.Checkout;


namespace Web.Controller.Api
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentApiController: ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public PaymentApiController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
            StripeConfiguration.ApiKey = "sk_test_51NAbKcKkVIc94TENfNN0LVe2TvNv1HdieqfHwxSi22LHaP5kl69Mt0fA3bTRwBzAaMgUSV4PyMYzlntiHmrr7k7O00BetHiXtn";
        }

        const string endpointSecret = "whsec_2596e314e0c85979f83b3cd60d54a3a8a42a4c7ba445ccdc0350cba034f8479a";
        
        [HttpPost("create-checkout-session")]
        public ActionResult<ClientSecretDto> CreateCheckoutSession()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _cartService.GetCart(userId);
            var lineItems = new List<SessionLineItemOptions>();
            foreach (var item in cart.CartItems)
            {
                var lineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long) item.MovieTicket.Price * 100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.MovieTicket.Movie.Title,
                        },
                    },
                    Quantity = item.Quantity,
                };
                lineItems.Add(lineItem);
            }
            var options = new SessionCreateOptions
            {

                LineItems = lineItems,
                Mode = "payment",
                UiMode = "embedded",
                ReturnUrl = "http://localhost:3000/thank-you",
            };
            

            var service = new SessionService();
            Session session = service.Create(options);

            return new ClientSecretDto { clientSecret = session.RawJObject["client_secret"]?.ToString() };
        }
        
        [HttpGet("finish-order")]
        public IActionResult FinishOrder()
        {
            var cart = _cartService.GetCart(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (cart.CartItems.Any())
            {
                _orderService.CreateOrder(cart);
            }
            return Ok();
        }
    }
}