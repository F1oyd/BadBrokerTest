using System;
using System.Collections.Generic;

namespace BadBrokerTest.Models
{
    public class ExternalRate
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }
}