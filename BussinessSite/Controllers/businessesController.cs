using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BussinessSite;

namespace BussinessSite.Controllers
{
    public class businessesController : Controller
    {
        private onLineEntities db = new onLineEntities();

        // GET: businesses
        public ActionResult Index()
        {
            return View(db.businesses.ToList());
        }

        // GET: businesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            businesses businesses = db.businesses.Find(id);
            if (businesses == null)
            {
                return HttpNotFound();
            }
            return View(businesses);
        }

        // GET: businesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: businesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "businessId,businessName,passward,kindOfPermission,Adress_city,Adress_street,Adress_numOfStreet,managerid")] businesses businesses)
        {
            if (ModelState.IsValid)
            {
                db.businesses.Add(businesses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(businesses);
        }

        // GET: businesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            businesses businesses = db.businesses.Find(id);
            if (businesses == null)
            {
                return HttpNotFound();
            }
            return View(businesses);
        }

        // POST: businesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "businessId,businessName,passward,kindOfPermission,Adress_city,Adress_street,Adress_numOfStreet,managerid")] businesses businesses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businesses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(businesses);
        }

        // GET: businesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            businesses businesses = db.businesses.Find(id);
            if (businesses == null)
            {
                return HttpNotFound();
            }
            return View(businesses);
        }

        // POST: businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            businesses businesses = db.businesses.Find(id);
            db.businesses.Remove(businesses);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
