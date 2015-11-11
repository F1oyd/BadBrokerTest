using System;
using System.ComponentModel.DataAnnotations;

namespace BadBrokerTest.Models
{
    public class Input
    {
        [Display(Name = "Date From")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Date Till")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateTill { get; set; }
        [Display(Name = "Amount, USD")]
        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
    }
}