using System;
using System.Collections.Generic;
using System.Linq;
using BadBrokerTest.Models;

namespace BadBrokerTest.Logic
{
    public static class RatesLogic
    {
        public static List<Rate> GetRates(DateTime dateFrom, DateTime dateTill)
        {
            var db = new RateContext();
            var localRates = db.Rates.Where(w => w.Date >= dateFrom && w.Date <= dateTill).ToList();
            for (var date = dateFrom; date <= dateTill; date = date.AddDays(1))
            {
                if (localRates.Select(s => s.Date).Contains(date)) continue;

                var externalRates = FixerGrabber.GetRates(date);
                if (!externalRates.Date.Equals(date)) continue;

                localRates.Add(new Rate
                {
                    Date = externalRates.Date,
                    RubRate = externalRates.Rates["RUB"],
                    EurRate = externalRates.Rates["EUR"],
                    GbpRate = externalRates.Rates["GBP"],
                    JpyRate = externalRates.Rates["JPY"]
                });
            }
            db.SaveChanges();

            return localRates;
        }
    }
}