using System;

namespace Domain.DTO
{
    public class PaymentDto
    {
        public Guid id { get; set; }
        public string description { get; set; }
        public int total { get; set; }
    }
}