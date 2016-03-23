using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Restaurant.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int DinersCount { get; set; }
        public string TimeIn { get; set; }
        public int TableID { get; set; }

        public virtual Table Table { get; set; }

    }
}