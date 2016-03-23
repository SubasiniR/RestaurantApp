using Restaurant.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant.Models
{
    public class Table
    {
        public int TableID { get; set; }
        public int ChairCount { get; set; }
        public bool Available { get; set; }
        public DateTime? AvailableAtTime { get; set; }

       
    }
}