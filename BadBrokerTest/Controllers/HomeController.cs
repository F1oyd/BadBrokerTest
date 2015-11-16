using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BadBrokerTest.Logic;
using BadBrokerTest.Models;

namespace BadBrokerTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly RateContext _db = new RateContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result(Input input)
        {
            List<Rate> rateList;
            try
            {
                rateList = RatesLogic.GetRates(input.DateFrom, input.DateTill, _db);
                ViewBag.Result = CalcLogic.FindBestOrders(rateList, input.Amount);
            }
            catch (Exception ex)
            {
                return PartialView("Error", ex.Message);
            }
            return PartialView(rateList);
        }
    }
}
