using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrokerSystem.Models;

namespace BrokerSystem.Controllers
{
    public class ShareholdersController : Controller
    {
        //
        // GET: /Shareholders/

        public ActionResult Index()
        {
            using (var db = new xmlDBOrigEntities1())
            {
                return View(db.share_holders.ToList());
            }
        }

        //
        // GET: /Shareholders/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Shareholders/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Shareholders/Create

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
        // GET: /Shareholders/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Shareholders/Edit/5

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
        // GET: /Shareholders/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Shareholders/Delete/5

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
