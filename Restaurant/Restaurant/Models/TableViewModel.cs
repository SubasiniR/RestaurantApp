using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant.Models
{
    public class TableViewModel
    {
        [Display(Name = "Table No.")]
        public int TableID { get; set; }

        [Display(Name = "Chair count")]
        public int ChairCount { get; set; }

        [Display(Name = "Available")]
        public bool Available { get; set; }

        [Display(Name = "Available At Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm tt}")]
        public DateTime? AvailableAtTime { get; set; }

    }
}