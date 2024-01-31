using System;
using System.Collections.Generic;
using System.Security.Claims;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller.Api
{
    [Authorize]
    [Route("api/cart")]
    [ApiController]
    public class CartApiController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ITicketService _ticketService;

        public CartApiController(ICartService cartService, ITicketService ticketService)
        {
            _cartService = cartService;
            _ticketService = ticketService;
        }
        
        [HttpGet]
        public ActionResult<CartDto> ViewCart()
        {
            //get current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //get cart
            var cart = _cartService.GetCart(userId);
            var cartItems = _cartService.GetCartItems(userId);

            var cartItemsDto = new List<OrderItemDto>();
            var total = 0;
            foreach (var item in cartItems)
            {
                cartItemsDto.Add(new OrderItemDto()
                {
                    Id = item.MovieTicket.Id,
                    MovieTitle = item.MovieTicket.Movie.Title,
                    Quantity = item.Quantity,
                    Price = item.MovieTicket.Price,
                });
                total += item.MovieTicket.Price * item.Quantity;
            }
            var cartDto = new CartDto()
            {
                OrderItems = cartItemsDto,
                TotalPrice = total,
                Id = cart.Id
            };
            return cartDto;
        }

        [HttpPost("{id:guid}")]
        public ActionResult<CartDto> AddToCart(Guid? id, [FromBody] int quantity)
        {
            //get current user
            var userId =   User.FindFirstValue(ClaimTypes.NameIdentifier);
            //get ticket
            var ticket = _ticketService.GetSpecificTicket(id);
            _cartService.AddToCart(userId, ticket, quantity);
            var cart = _cartService.GetCart(userId);
            return new CartDto()
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                OrderItems = new List<OrderItemDto>(),
                UserId = userId

            };
        }
        
        [HttpDelete("{id:guid}")]
        public ActionResult<CartDto> RemoveFromCart(Guid? id)
        {
            //get current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //get ticket
            var ticket = _ticketService.GetSpecificTicket(id);
            _cartService.DeleteFromCart(userId, ticket);
            var cart = _cartService.GetCart(userId);
            return new CartDto()
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                OrderItems = new List<OrderItemDto>(),
                UserId = userId

            };

        }
        
        [HttpDelete("deleteAll")]
        public ActionResult<CartDto> RemoveAllFromCart()
        {
            //get current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.DeleteAllFromCart(userId);

            var cart = _cartService.GetCart(userId);
            return new CartDto()
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                OrderItems = new List<OrderItemDto>(),
                UserId = userId

            };
        }

        [HttpPut("{id:guid}")]
        public ActionResult<CartDto> UpdateAmount(Guid? id, [FromBody] int amount)
        {
            //get current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //get ticket
            var ticket = _ticketService.GetSpecificTicket(id);
            _cartService.ChangeQuantity(userId, ticket, amount);
            var cart = _cartService.GetCart(userId);
            return new CartDto()
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                OrderItems = new List<OrderItemDto>(),
                UserId = userId

            };
        }
        
        
        
    }
}