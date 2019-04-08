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
    public class StockHistoriesController : Controller
    {
        private StockDataEntities db = new StockDataEntities();

        // GET: StockHistories
        public ActionResult Index()
        {
            return View(db.StockHistories.ToList());
        }

        // GET: StockHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockHistory stockHistory = db.StockHistories.Find(id);
            if (stockHistory == null)
            {
                return HttpNotFound();
            }
            return View(stockHistory);
        }

        // GET: StockHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: StockHistories/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Symbol,LastPrice,Change,ChangePercent,MarketTime,Volume,AvgVol,Shares,MarketCap")] StockHistory stockHistory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.StockHistories.Add(stockHistory);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(stockHistory);
        //}

        //// GET: StockHistories/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StockHistory stockHistory = db.StockHistories.Find(id);
        //    if (stockHistory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(stockHistory);
        //}

        //// POST: StockHistories/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Symbol,LastPrice,Change,ChangePercent,MarketTime,Volume,AvgVol,Shares,MarketCap")] StockHistory stockHistory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(stockHistory).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(stockHistory);
        //}

        // GET: StockHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockHistory stockHistory = db.StockHistories.Find(id);
            if (stockHistory == null)
            {
                return HttpNotFound();
            }
            return View(stockHistory);
        }

        // POST: StockHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockHistory stockHistory = db.StockHistories.Find(id);
            db.StockHistories.Remove(stockHistory);
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
