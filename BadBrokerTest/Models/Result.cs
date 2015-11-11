using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadBrokerTest.Models
{
    public class Result
    {
        public string Currency { get; set; }
        public DateTime BuyDate { get; set; }
        public DateTime SellDate { get; set; }
        public double Revenue { get; set; }
    }
}