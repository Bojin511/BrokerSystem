using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrokerSystem.Models;

namespace BrokerSystem.Controllers
{
    public class PortfolioController : Controller
    {
        //
        // GET: /Portfolio/


        public ActionResult Index()
        {
            string Username = User.Identity.Name;

            using (var db = new xmlDBOrigEntities1())
            {
                string firstName = db.Users.Where(x => x.username == Username).Select(x => x.first_name).FirstOrDefault();
                string lastName = db.Users.Where(x => x.username == Username).Select(x => x.last_name).FirstOrDefault();
                int bid = db.brokers.Where(x => ((x.first_name == firstName) && (x.last_name == lastName))).Select(x => x.broker_id).FirstOrDefault();

                var stockExList = from b in db.brokers
                                  join bse in db.broker_stock_ex on b.broker_id equals bse.broker_id
                                  join se in db.stock_exchanges on bse.stock_ex_id equals se.stock_ex_id
                                  join p in db.places on se.place_id equals p.place_id
                                  where b.broker_id == 1
                                  select se;

                var stock = stockExList.Distinct();

                ViewBag.stock = stock.ToList();


                var registeredShareholders = from c in db.share_holder_broker
                                             join sh in db.share_holders
                                             on c.share_holder_id equals sh.share_holder_id
                                             join b in db.brokers on c.broker_id equals b.broker_id
                                             where c.broker_id == bid
                                             select sh;

                var registeredShareholder = registeredShareholders.Distinct();

                ViewBag.shareholder = registeredShareholder.ToList();

                return View();
            }

        }

        public ActionResult RegisterStockEx()
        {

            using (var db = new xmlDBOrigEntities1())
            {
                var query = from c in db.stock_exchanges
                            select new
                            {
                                c.stock_ex_id,
                                c.name
                            };
                ViewData["stockExchanges"] = query.AsEnumerable()
                                 .Select(c => new SelectListItem
                                 {
                                     Value = c.stock_ex_id.ToString(),
                                     Text = c.name,
                                     Selected = false
                                 })
                                 .ToList();
            }

            return View();



        }

        public ActionResult AddToBrokerStockEx()
        {
            string selected = Request["stockExchanges"];
            if (selected == "")
            {
                return RedirectToAction("RegisterStockEx");
            }
            else
            {
                using (var db = new xmlDBOrigEntities1())
                {
                    int bid = GetCurrentBroker().broker_id;

                    int cid = Convert.ToInt32(selected);
                    var bfc = new broker_stock_ex { stock_ex_id = cid, broker_id = bid };
                    db.broker_stock_ex.AddObject(bfc);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult DeleteStockEx(int stockExId)
        {
            brokers broker = GetCurrentBroker();

            using (var db = new xmlDBOrigEntities1())
            {
                var stock_exs =
                        from bse in db.broker_stock_ex
                        where bse.broker_id == broker.broker_id
                        where bse.stock_ex_id == stockExId
                        select bse;

                foreach (var stock_ex in stock_exs)
                {
                    db.broker_stock_ex.DeleteObject(stock_ex);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult RegisterShareholder()
        {

            using (var db = new xmlDBOrigEntities1())
            {
                var query = from c in db.share_holders
                            select new
                            {
                                c.share_holder_id,
                                c.first_name,
                                c.last_name
                            };
                ViewData["shareholderList"] = query.AsEnumerable()
                                 .Select(c => new SelectListItem
                                 {
                                     Value = c.share_holder_id.ToString(),
                                     Text = c.first_name + " " + c.last_name,
                                     Selected = false
                                 })
                                 .ToList();
            }

            return View();



        }

        public ActionResult AddRegisteredShareholder()
        {
            string selected = Request["shareholderList"];
            if (selected == "")
            {
                return RedirectToAction("RegisterShareholder");
            }
            else
            {
                using (var db = new xmlDBOrigEntities1())
                {
                    int bid = GetCurrentBroker().broker_id;

                    int shid = Convert.ToInt32(selected);
                    var shb = new share_holder_broker { share_holder_id = shid, broker_id = bid };
                    db.share_holder_broker.AddObject(shb);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult DeleteFromRegisteredShareholder(int shareholderID)
        {
            brokers broker = GetCurrentBroker();

            using (var db = new xmlDBOrigEntities1())
            {
                var shareholder =
                        from shb in db.share_holder_broker
                        where shb.broker_id == broker.broker_id
                        where shb.share_holder_id == shareholderID
                        select shb;

                foreach (var oneShareholder in shareholder)
                {
                    db.share_holder_broker.DeleteObject(oneShareholder);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }





        public brokers GetCurrentBroker()
        {
            string Username = User.Identity.Name;

            using (var db = new xmlDBOrigEntities1())
            {

                string firstName = db.Users.Where(x => x.username == Username).Select(x => x.first_name).FirstOrDefault();
                string lastName = db.Users.Where(x => x.username == Username).Select(x => x.last_name).FirstOrDefault();
                brokers broker = db.brokers.Where(x => ((x.first_name == firstName) && (x.last_name == lastName))).FirstOrDefault();

                return broker;
            }
        }

    }
}

    

