using System;
using System.Collections.Generic;
using System.Linq;
using BadBrokerTest.Models;

namespace BadBrokerTest.Logic
{
    public static class RatesLogic
    {
        public static List<Rate> GetRates(DateTime dateFrom, DateTime dateTill, RateContext db)
        {
            var localRates = db.Rates.Where(w => w.Date >= dateFrom && w.Date <= dateTill);
            for (var date = dateFrom; date <= dateTill; date = date.AddDays(1))
            {
                if (localRates.Any(s => s.Date.Equals(date))) continue;

                var externalRates = FixerGrabber.GetRates(date);
                if (!externalRates.Date.Equals(date)) continue;

                var rate = new Rate
                {
                    Date = externalRates.Date,
                    RubRate = externalRates.Rates["RUB"],
                    EurRate = externalRates.Rates["EUR"],
                    GbpRate = externalRates.Rates["GBP"],
                    JpyRate = externalRates.Rates["JPY"]
                };

                db.Entry(rate).State = System.Data.EntityState.Added;
            }
            db.SaveChanges();

            return localRates.ToList();
        }
    }
}