using System;

namespace Domain.DTO
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public string MovieTitle { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}