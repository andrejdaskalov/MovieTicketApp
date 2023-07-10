using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class CartDto
    {
        public IList<OrderItemDto> OrderItems { get; set; }
        public int TotalPrice { get; set; }
        public Guid Id { get; set; }
        public string UserId { get; set; }
        
    }
}