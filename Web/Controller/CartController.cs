using System;
using System.Collections.Generic;
using System.Security.Claims;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller
{
    [Authorize]
    public class CartController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICartService _cartService;
        private readonly ITicketService _ticketService;

        public CartController(ICartService cartService, ITicketService ticketService)
        {
            _cartService = cartService;
            _ticketService = ticketService;
        }
        
        [HttpGet]
        public IActionResult ViewCart()
        {
            //get current user
            var userId = User.FindFirstValue(ClaimTypes.Name);
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
            return View(cartDto);
        }

        [HttpPost]
        public IActionResult AddToCart(Guid? id, int quantity)
        {
            //get current user
            var userId = User.FindFirstValue(ClaimTypes.Name);
            //get ticket
            var ticket = _ticketService.GetSpecificTicket(id);
            _cartService.AddToCart(userId, ticket, quantity);

            return RedirectToAction("Index", "Ticket");
        }
        
        [HttpGet]
        public IActionResult RemoveFromCart(Guid? id)
        {
            //get current user
            var userId = User.FindFirstValue(ClaimTypes.Name);
            //get ticket
            var ticket = _ticketService.GetSpecificTicket(id);
            _cartService.DeleteFromCart(userId, ticket);

            return RedirectToAction("ViewCart", "Cart");
        }
        
        [HttpGet]
        public IActionResult RemoveAllFromCart()
        {
            //get current user
            var userId = User.FindFirstValue(ClaimTypes.Name);
            _cartService.DeleteAllFromCart(userId);

            return RedirectToAction("ViewCart", "Cart");
        }

        [HttpPost]
        public IActionResult UpdateAmount(Guid? id, int amount)
        {
            //get current user
            var userId = User.FindFirstValue(ClaimTypes.Name);
            //get ticket
            var ticket = _ticketService.GetSpecificTicket(id);
            _cartService.ChangeQuantity(userId, ticket, amount);

            return RedirectToAction("ViewCart", "Cart");
        }
        
        
        
    }
}