using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrokerSystem.Models;

namespace BrokerSystem.Controllers
{
    public class TradeController : Controller
    {
        //
        // GET: /Trade/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DoTrade()
        {
            using (var db = new xmlDBOrigEntities1())
            {
                companyListQuery(db);
                shareholderListQuery(db);
                stockExListQuery(db);
                transactionTypeListQuery(db);
            }
            return View();
        }

        private void companyListQuery(xmlDBOrigEntities1 db)
        {
            var query = from c in db.companies
                        join s in db.shares
                        on c.company_id equals s.company_id
                        select new
                        {
                            c.company_id,
                            c.name,
                            s.share_id
                        };
            ViewData["companies"] = query.AsEnumerable()
                             .Select(c => new SelectListItem
                             {
                                 Value = c.share_id.ToString(),
                                 Text = c.name,
                                 Selected = false
                             })
                             .ToList();
        }

        private void shareholderListQuery(xmlDBOrigEntities1 db)
        {
            var query = from s in db.share_holders
                        select new
                        {
                            s.first_name,
                            s.last_name,
                            s.share_holder_id
                        };
            ViewData["shareholders"] = query.AsEnumerable()
                             .Select(c => new SelectListItem
                             {
                                 Value = c.share_holder_id.ToString(),
                                 Text = c.first_name + " "+ c.last_name,
                                 Selected = false
                             })
                             .ToList();
        }


        private void stockExListQuery(xmlDBOrigEntities1 db)
        {
            var query = from s in db.stock_exchanges
                        select new
                        {
                            s.name,
                            s.stock_ex_id
                        };
            ViewData["stockEx"] = query.AsEnumerable()
                             .Select(c => new SelectListItem
                             {
                                 Value = c.stock_ex_id.ToString(),
                                 Text = c.name,
                                 Selected = false
                             })
                             .ToList();
        }

        private void transactionTypeListQuery(xmlDBOrigEntities1 db)
        {
            var query = from s in db.transaction_types
                        select new
                        {
                            s.type_id,
                            s.transaction_type
                        };
            ViewData["transactionType"] = query.AsEnumerable()
                             .Select(c => new SelectListItem
                             {
                                 Value = c.type_id.ToString(),
                                 Text = c.transaction_type,
                                 Selected = false
                             })
                             .ToList();
        }

        [HttpPost]
        public ActionResult DoTrade( int? share_amount)
        {
            string Username = User.Identity.Name;

            using (var db = new xmlDBOrigEntities1())
            {
                string firstName = db.Users.Where(x => x.username == Username).Select(x => x.first_name).FirstOrDefault();
                string lastName = db.Users.Where(x => x.username == Username).Select(x => x.last_name).FirstOrDefault();
                int bid = db.brokers.Where(x => ((x.first_name == firstName) && (x.last_name == lastName))).Select(x => x.broker_id).FirstOrDefault();

                string selectedCompany = Request["companies"];
                string selectedShareholder = Request["shareholders"];
                string selectedStockEx = Request["stockEx"];
                string transactionType = Request["transactionType"];

                int share_id = Convert.ToInt32(selectedCompany);
                int shareholder_id = Convert.ToInt32(selectedShareholder);
                int stockEx_id = Convert.ToInt32(selectedStockEx);
                int transaction_id = Convert.ToInt32(transactionType);

                var query = (from s in db.shares_prices
                            where s.share_id == share_id
                            orderby s.time_start descending
                            select s.price).Take(1);

                var currentPrice = query.ToList()[0];

                DateTime value = new DateTime(2010, 1, 18);
                value = DateTime.Today;

                int price = Convert.ToInt32(currentPrice);
                int amount = Convert.ToInt32(share_amount);
                int price_total = price * amount;
                

                var newTrade = new trades 
                { 
                  share_id = share_id, 
                  broker_id = bid, 
                  share_holder_id = shareholder_id,  
                  stock_ex_id = stockEx_id,
                  transaction_type = transaction_id,
                  transaction_time = value,
                  share_amount = amount,
                  price_total = price_total
                };
                db.trades.AddObject(newTrade);
                db.SaveChanges();



                ViewBag.Message = "Trade has been done!";

                companyListQuery(db);
                shareholderListQuery(db);
                stockExListQuery(db);
                transactionTypeListQuery(db);

                return View();
            }
        }
    }
}

    

