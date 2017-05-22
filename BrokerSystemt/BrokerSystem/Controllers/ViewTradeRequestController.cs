using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrokerSystem.Models;

namespace BrokerSystem.Controllers
{
    public class ViewTradeRequestController : Controller
    {
        //
        // GET: /TradeRequest/

        public ActionResult Index()
        {
            try
            {
                using (var db = new xmlDBOrigEntities1())
                {
                    string Username = User.Identity.Name;

                    string firstName = db.Users.Where(x => x.username == Username).Select(x => x.first_name).FirstOrDefault();
                    string lastName = db.Users.Where(x => x.username == Username).Select(x => x.last_name).FirstOrDefault();
                    int bid = db.brokers.Where(x => ((x.first_name == firstName) && (x.last_name == lastName))).Select(x => x.broker_id).FirstOrDefault();

                    var Query = from t in db.share_holder_trade_request
                                 join s in db.shares on t.share_id equals s.share_id
                                 join c in db.companies on s.company_id equals c.company_id
                                 join sh in db.share_holders on t.share_holder_id equals sh.share_holder_id
                                 join se in db.stock_exchanges on t.stock_ex_id equals se.stock_ex_id
                                 join tty in db.transaction_types on t.transaction_type equals tty.type_id
                                 where t.broker_id == bid
                                 select new shareholderRequest{ companyName = c.name, shareholderName = sh.first_name + " " + sh.last_name, stockEx = se.name, transactionType = tty.transaction_type, shareAmount = t.share_amount };

                    var ShareholderRequests = Query.Take(8);

                    ViewBag.ShareholderRequests = ShareholderRequests.ToList();
                }
                return View();

            }

            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return View();
            }

        }

        //
        // GET: /TradeRequest/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TradeRequest/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TradeRequest/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /TradeRequest/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TradeRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TradeRequest/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TradeRequest/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
