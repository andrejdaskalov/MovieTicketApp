using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class TicketDto
    {
        public IEnumerable<Movie> MovieOptions { get; set; }
        public Guid SelectedMovie { get; set; }
        public string MovieTitle { get; set; }
        public Guid Id { get; set; }
        public string Seat { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
        
    }
}