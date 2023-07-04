using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class MovieTicket : BaseEntity
    {
        public Movie Movie { get; set; }
        // public IdentityUser User { get; set; }
        public DateTime Date { get; set; }
        public string Seat { get; set; }
        public int Price { get; set; }
    }
}