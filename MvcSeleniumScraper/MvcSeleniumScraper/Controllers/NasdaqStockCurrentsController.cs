using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcSeleniumScraper.Models;

namespace MvcSeleniumScraper.Controllers
{
    public class NasdaqStockCurrentsController : Controller
    {
        private StockDataEntities1 db = new StockDataEntities1();

        // GET: NasdaqStockCurrents
        public ActionResult Index()
        {
            return View(db.NasdaqStockCurrents.ToList());
        }

        // GET: NasdaqStockCurrents/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NasdaqStockCurrent nasdaqStockCurrent = db.NasdaqStockCurrents.Find(id);
            if (nasdaqStockCurrent == null)
            {
                return HttpNotFound();
            }
            return View(nasdaqStockCurrent);
        }

        // GET: NasdaqStockCurrents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NasdaqStockCurrents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Symbol,Price,Change")] NasdaqStockCurrent nasdaqStockCurrent)
        {
            if (ModelState.IsValid)
            {
                db.NasdaqStockCurrents.Add(nasdaqStockCurrent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nasdaqStockCurrent);
        }

        // GET: NasdaqStockCurrents/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NasdaqStockCurrent nasdaqStockCurrent = db.NasdaqStockCurrents.Find(id);
            if (nasdaqStockCurrent == null)
            {
                return HttpNotFound();
            }
            return View(nasdaqStockCurrent);
        }

        // POST: NasdaqStockCurrents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Symbol,Price,Change")] NasdaqStockCurrent nasdaqStockCurrent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nasdaqStockCurrent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nasdaqStockCurrent);
        }

        // GET: NasdaqStockCurrents/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NasdaqStockCurrent nasdaqStockCurrent = db.NasdaqStockCurrents.Find(id);
            if (nasdaqStockCurrent == null)
            {
                return HttpNotFound();
            }
            return View(nasdaqStockCurrent);
        }

        // POST: NasdaqStockCurrents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NasdaqStockCurrent nasdaqStockCurrent = db.NasdaqStockCurrents.Find(id);
            db.NasdaqStockCurrents.Remove(nasdaqStockCurrent);
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
