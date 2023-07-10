using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.DTO;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Service;
using ComponentInfo = GemBox.Document.ComponentInfo;

namespace Web.Controller
{
    public class OrderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        // GET
        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders().ToList();
            var ordersDto = new List<CartDto>();
            foreach (var order in orders)
            {

                var orderDto = new CartDto();
                orderDto.OrderItems = new List<OrderItemDto>();
                foreach (var item in order.OrderItems.ToList())
                {
                   var orderItemDto = new OrderItemDto
                   {
                       Id = item.Id,
                       MovieTitle = item.MovieTicket.Movie.Title,
                       Price = item.MovieTicket.Price,
                       Quantity = item.Quantity
                   }; 
                   orderDto.OrderItems.Add(orderItemDto);
                }
                orderDto.Id = order.Id;
                orderDto.UserId = order.User.UserName;
                orderDto.TotalPrice = order.TotalPrice;
                ordersDto.Add(orderDto);
            }
            return View(ordersDto);
        }

        public IActionResult CreateInvoice(Guid id)
        {
            var order = _orderService.GetOrderById(id);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");

            var document = DocumentModel.Load(templatePath);
            
            document.Content.Replace("{{OrderNumber}}",order.Id.ToString());
            document.Content.Replace("{{CustomerEmail}}",order.User.UserName);
            document.Content.Replace("{{TotalPrice}}",order.TotalPrice.ToString());
            
            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportedInvoice.pdf");
        }
    }
}