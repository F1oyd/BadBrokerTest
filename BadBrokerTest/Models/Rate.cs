using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace BadBrokerTest.Models
{
    public class Rate
    {
        [Key]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        [Display(Name = "USDRUB Cross")]
        public double RubRate { get; set; }
        [Display(Name = "USDEUR Cross")]
        public double EurRate { get; set; }
        [Display(Name = "USDGBP Cross")]
        public double GbpRate { get; set; }
        [Display(Name = "USDJPY Cross")]
        public double JpyRate { get; set; }
    }

    public class RateContext : DbContext
    {
        public DbSet<Rate> Rates { get; set; }
    }
}