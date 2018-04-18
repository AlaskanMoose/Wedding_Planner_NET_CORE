using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models{
    public class Wedding : BaseEntity{
        public int WeddingId { get; set; }
        public string WedderOne { get; set; } 
        public string WedderTwo { get; set; } 
        public string Address {get; set; }
        public DateTime WeddingDate {get; set; }
        public DateTime CreatedAt{get; set; }

        public List<GuestList> Guests { get; set; }
        public Wedding(){
            Guests = new List<GuestList>();
        }
    }
}