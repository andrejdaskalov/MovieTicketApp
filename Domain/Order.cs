using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Order : BaseEntity
    {
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual IdentityUser User { get; set; }
        public int TotalPrice { get; set; }
    }
}