using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrokerSystem.Models;

namespace BrokerSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Trading System!";

            return View();
        }

        public ActionResult CompanyPriceList()
        {
            try
            {
                using (var db = new xmlDBOrigEntities1())
                {
                    var Prices = from c in db.companies
                                          join p in db.places on c.place_id equals p.place_id
                                          join s in db.shares on c.company_id equals s.company_id
                                          join sa in db.shares_amounts on s.share_id equals sa.share_id
                                          join sp in db.shares_prices on s.share_id equals sp.share_id
                                          join cu in db.currencies on s.currency_id equals cu.currency_id
                                          orderby sp.time_start descending
                                          select new CompanyPrice { name = c.name, city = p.city, country = p.country, issued = sa.issued, shares_date_start = sa.date_start, shares_date_end = sa.date_end, price = sp.price, price_time_start = sp.time_start, price_time_end = sp.time_end, symbol = cu.symbol };

                    var CompaniesPrices = Prices.Take(8);

                    ViewBag.CompaniesPrices = CompaniesPrices.ToList();
                }
                    return View();
            
            }

            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return View();
            }

        }
    }
}
    

