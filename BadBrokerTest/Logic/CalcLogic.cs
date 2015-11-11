using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BadBrokerTest.Models;

namespace BadBrokerTest.Logic
{
    public static class CalcLogic
    {
        public static Result FindBestOrders(IList<Rate> rateList, double amount)
        {
            var result = new Result {Revenue = double.MinValue};
            for (var i = 0; i < rateList.Count-1; i++)
            {
                for (var j = i+1; j < rateList.Count; j++)
                {
                    var revenue = (rateList[i].RubRate * amount / rateList[j].RubRate) -
                                 (rateList[j].Date - rateList[i].Date).Days*1;

                    if (revenue > result.Revenue)
                        result = new Result
                        {
                            Currency = "USDRUB Cross",
                            BuyDate = rateList[i].Date,
                            SellDate = rateList[j].Date,
                            Revenue = revenue
                        };

                    revenue = (rateList[i].EurRate * amount / rateList[j].EurRate) -
                                 (rateList[j].Date - rateList[i].Date).Days * 1;

                    if (revenue > result.Revenue)
                        result = new Result
                        {
                            Currency = "USDEUR Cross",
                            BuyDate = rateList[i].Date,
                            SellDate = rateList[j].Date,
                            Revenue = revenue
                        };

                    revenue = (rateList[i].GbpRate * amount / rateList[j].GbpRate) -
                                 (rateList[j].Date - rateList[i].Date).Days * 1;

                    if (revenue > result.Revenue)
                        result = new Result
                        {
                            Currency = "USDGBP Cross",
                            BuyDate = rateList[i].Date,
                            SellDate = rateList[j].Date,
                            Revenue = revenue
                        };

                    revenue = (rateList[i].JpyRate * amount / rateList[j].JpyRate) -
                                 (rateList[j].Date - rateList[i].Date).Days * 1;

                    if (revenue > result.Revenue)
                        result = new Result
                        {
                            Currency = "USDJPY Cross",
                            BuyDate = rateList[i].Date,
                            SellDate = rateList[j].Date,
                            Revenue = revenue
                        };
                }
            }
            return result;
        }
    }
}