using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Cart : BaseEntity
    {
        public virtual List<OrderItem> CartItems { get; set; }
        public virtual IdentityUser User { get; set; }
        public int TotalPrice { get; set; }
    }
}