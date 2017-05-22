using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BrokerSystem.Models;
using System.Data.Objects.SqlClient;



namespace BrokerSystem.Controllers
{
    public class UserPreferencesController : Controller
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

                var favouriteCompany = from c in db.companies
                                       join bfc in db.broker_favourite_companies on c.company_id equals bfc.company_id
                                       join b in db.brokers on bfc.broker_id equals b.broker_id
                                       where bfc.broker_id == bid
                                       select c;

                var favouriteCompanies = favouriteCompany.Distinct();

                ViewBag.joined = favouriteCompanies.ToList();

                var watchList = from bwl in db.broker_watch_list
                                join c in db.companies on bwl.company_id equals c.company_id
                                join b in db.brokers on bwl.broker_id equals b.broker_id
                                where bwl.broker_id == bid
                                select c;
                var watchLists = watchList.Distinct();

                ViewBag.watchList = watchLists.ToList();

                var commission = from bcr in db.broker_commission_rate
                                 where bcr.broker_id == bid
                                 select bcr.rate;

                ViewBag.commission = commission.ToList();



                return View();
            }

        }


        public ActionResult CreateFavouriteCompanies()
        {

            using (var db = new xmlDBOrigEntities1())
            {
                var query = from c in db.companies
                            select new
                            {
                                c.company_id,
                                c.name
                            };
                ViewData["companies"] = query.AsEnumerable()
                                 .Select(c => new SelectListItem
                                 {
                                     Value = c.company_id.ToString(),
                                     Text = c.name,
                                     Selected = false
                                 })
                                 .ToList();
            }

            return View();
        }

        public ActionResult AddToBrokerFavourite()
        {
            string selected = Request["companies"];
            if (selected == "")
            {
                return RedirectToAction("CreateFavouriteCompanies");
            }
            else
            {
                string Username = User.Identity.Name;

                using (var db = new xmlDBOrigEntities1())
                {
                    string firstName = db.Users.Where(x => x.username == Username).Select(x => x.first_name).FirstOrDefault();
                    string lastName = db.Users.Where(x => x.username == Username).Select(x => x.last_name).FirstOrDefault();
                    int bid = db.brokers.Where(x => ((x.first_name == firstName) && (x.last_name == lastName))).Select(x => x.broker_id).FirstOrDefault();

                    int cid = Convert.ToInt32(selected);
                    var bfc = new broker_favourite_companies { company_id = cid, broker_id = bid };
                    db.broker_favourite_companies.AddObject(bfc);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult DeleteFavouriteCompany(int companyID)
        {
            brokers broker = GetCurrentBroker();

            using (var db = new xmlDBOrigEntities1())
            {
                var companies = from c in db.broker_favourite_companies
                                where c.broker_id == broker.broker_id
                                where c.company_id == companyID
                                select c;
                foreach (var com in companies)
                {
                    db.DeleteObject(com);
                }

                db.SaveChanges();



            }

            return RedirectToAction("Index");
        }


        public ActionResult CreateWatchList(broker_watch_list newCompanyWatchList)
        {

            using (var db = new xmlDBOrigEntities1())
            {
                var query = from c in db.companies
                            select new
                            {
                                c.company_id,
                                c.name
                            };
                ViewData["companiesList"] = query.AsEnumerable()
                                 .Select(c => new SelectListItem
                                 {
                                     Value = c.company_id.ToString(),
                                     Text = c.name,
                                     Selected = false
                                 })
                                 .ToList();
            }

            return View();
        }

        public ActionResult AddToWatchList()
        {
            string selectedCompany = Request["companiesList"];
            if (selectedCompany == "")
            {
                return RedirectToAction("CreateWatchList");
            }
            else
            {
                string Username = User.Identity.Name;

                using (var db = new xmlDBOrigEntities1())
                {
                    string firstName = db.Users.Where(x => x.username == Username).Select(x => x.first_name).FirstOrDefault();
                    string lastName = db.Users.Where(x => x.username == Username).Select(x => x.last_name).FirstOrDefault();
                    int bid = db.brokers.Where(x => ((x.first_name == firstName) && (x.last_name == lastName))).Select(x => x.broker_id).FirstOrDefault();

                    int cid = Convert.ToInt32(selectedCompany);
                    var bwl = new broker_watch_list { company_id = cid, broker_id = bid };
                    db.broker_watch_list.AddObject(bwl);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult DeleteFromWatchList(int companyID)
        {
            brokers broker = GetCurrentBroker();

            using (var db = new xmlDBOrigEntities1())
            {
                var companies = from c in db.broker_watch_list
                                where c.broker_id == broker.broker_id
                                where c.company_id == companyID
                                select c;
                foreach (var com in companies)
                {
                    db.DeleteObject(com);
                }

                db.SaveChanges();

            }

            return RedirectToAction("Index");
        }


        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(double rate)
        {
            string Username = User.Identity.Name;

            using (var db = new xmlDBOrigEntities1())
            {
                string firstName = db.Users.Where(x => x.username == Username).Select(x => x.first_name).FirstOrDefault();
                string lastName = db.Users.Where(x => x.username == Username).Select(x => x.last_name).FirstOrDefault();
                int bid = db.brokers.Where(x => ((x.first_name == firstName) && (x.last_name == lastName))).Select(x => x.broker_id).FirstOrDefault();

                var bcr = from c in db.broker_commission_rate
                          where c.broker_id == bid
                          select c;
                foreach (var b in bcr)
                {
                    db.DeleteObject(b);
                }
                var brokerRate = new broker_commission_rate { rate = rate, broker_id = bid };
                db.broker_commission_rate.AddObject(brokerRate);
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




