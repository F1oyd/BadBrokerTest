using System.Web.Mvc;
using BadBrokerTest.Logic;
using BadBrokerTest.Models;

namespace BadBrokerTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result(Input input)
        {
            if (!ModelState.IsValid) return PartialView("Error");

            var rateList = RatesLogic.GetRates(input.DateFrom, input.DateTill);
            ViewBag.Result = CalcLogic.FindBestOrders(rateList, input.Amount);
            return PartialView(rateList);
        }
    }
}
