using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrokerSystem.Models;

namespace BrokerSystem.Controllers
{
    public class SendUserRequestController : Controller
    {
        //
        // GET: /SendUserRequest/Index

        public ActionResult Index()
        {
            return View();
        } 

        //
        // POST: /SendUserRequest/Index

        [HttpPost]
        public ActionResult Index(UserRequests newUserRequest)
        {
            try
            {
                using (var db = new xmlDBOrigEntities1())
                {
                    db.UserRequests.AddObject(newUserRequest);
                    db.SaveChanges();
                }
            
                ViewBag.Message = "User Request has been sent!";

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
