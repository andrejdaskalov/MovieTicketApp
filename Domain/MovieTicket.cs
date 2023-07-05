using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class MovieTicket : BaseEntity
    {
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public DateTime Date { get; set; }
        public string Seat { get; set; }
        public int Price { get; set; }
    }
}