using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal Price { get; set; }
    }
    
}