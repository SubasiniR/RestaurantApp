using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer No.")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Count")]
        public int DinersCount { get; set; }

        [Display(Name = "Time In")]
        public string TimeIn { get; set; }
                
        [Display(Name = "Table Allotted") ]
        public int TableID { get; set; }

        public virtual Table Table { get; set; }

    }
}