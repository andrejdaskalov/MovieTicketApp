using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Order : BaseEntity
    {
        public ICollection<OrderItem> OrderItems { get; set; }
        public IdentityUser User { get; set; }
    }
}