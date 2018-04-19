using System;

namespace DAL.Core.Models
{
    internal class Event 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal StudentPrice { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }   
    }
}
