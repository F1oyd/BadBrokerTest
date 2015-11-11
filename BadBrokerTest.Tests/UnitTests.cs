using System;
using BadBrokerTest.Logic;
using BadBrokerTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BadBrokerTest.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestFixerGrabber()
        {
            var rates = FixerGrabber.GetRates(DateTime.Today.AddDays(-1));
            Assert.AreEqual(rates.Date, DateTime.Today.AddDays(-1));
        }

        [TestMethod]
        public void TestCalcLogic1()
        {
            var x = CalcLogic.FindBestOrders(new[]
            {
                new Rate {Date = new DateTime(2014, 12, 16), RubRate = 73d},
                new Rate {Date = new DateTime(2014, 12, 22), RubRate = 54.781d}
            }, 100d);
            Assert.AreEqual(Math.Round(x.Revenue, 2), 127.26d);
        }
        [TestMethod]
        public void TestCalcLogic2()
        {
            var x = CalcLogic.FindBestOrders(new[]
            {
                new Rate {Date = new DateTime(2012, 1, 5), RubRate = 40d},
                new Rate {Date = new DateTime(2012, 1, 7), RubRate = 35d},
                new Rate {Date = new DateTime(2012, 1, 19), RubRate = 30d}
            }, 50d);
            Assert.AreEqual(Math.Round(x.Revenue, 2), 55.6d);
        }
    }
}
