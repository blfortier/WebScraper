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
    public class WorldTradeStockHistoriesController : Controller
    {
        private StockDataEntities1 db = new StockDataEntities1();

        // GET: WorldTradeStockHistories
        public ActionResult Index()
        {
            return View(db.WorldTradeStockHistories.ToList());
        }

        // GET: WorldTradeStockHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorldTradeStockHistory worldTradeStockHistory = db.WorldTradeStockHistories.Find(id);
            if (worldTradeStockHistory == null)
            {
                return HttpNotFound();
            }
            return View(worldTradeStockHistory);
        }

        // GET: WorldTradeStockHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorldTradeStockHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Symbol,Price,Change,ChangePercent")] WorldTradeStockHistory worldTradeStockHistory)
        {
            if (ModelState.IsValid)
            {
                db.WorldTradeStockHistories.Add(worldTradeStockHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(worldTradeStockHistory);
        }

        // GET: WorldTradeStockHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorldTradeStockHistory worldTradeStockHistory = db.WorldTradeStockHistories.Find(id);
            if (worldTradeStockHistory == null)
            {
                return HttpNotFound();
            }
            return View(worldTradeStockHistory);
        }

        // POST: WorldTradeStockHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Symbol,Price,Change,ChangePercent")] WorldTradeStockHistory worldTradeStockHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worldTradeStockHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(worldTradeStockHistory);
        }

        // GET: WorldTradeStockHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorldTradeStockHistory worldTradeStockHistory = db.WorldTradeStockHistories.Find(id);
            if (worldTradeStockHistory == null)
            {
                return HttpNotFound();
            }
            return View(worldTradeStockHistory);
        }

        // POST: WorldTradeStockHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorldTradeStockHistory worldTradeStockHistory = db.WorldTradeStockHistories.Find(id);
            db.WorldTradeStockHistories.Remove(worldTradeStockHistory);
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
