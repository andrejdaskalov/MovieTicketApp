using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class TicketDto
    {
        // public IEnumerable<Movie> MovieOptions { get; set; }
        public string SelectedMovie { get; set; }
        public string MovieTitle { get; set; }
        public string Id { get; set; }
        public string Seat { get; set; }
        public string Price { get; set; }
        public DateTime Date { get; set; }
        
    }
}