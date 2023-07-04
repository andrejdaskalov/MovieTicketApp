﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; } 
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string TrailerUrl { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
    }
}