using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcSeleniumScraper.Models;
using MvcSeleniumScraper.RestSharpScraperService;

namespace MvcSeleniumScraper.Controllers
{
    [Authorize]
    public class WorldTradeStockCurrentsController : Controller
    {
        private StockDataEntities4 db = new StockDataEntities4();

        // GET: WorldTradeStockCurrents
        public ActionResult Index()
        {
            return View(db.WorldTradeStockCurrents.ToList());
        }


        [Authorize]
        public ActionResult NewScrape()
        {
            if (ModelState.IsValid)
            {
                var apiCall = new ApiCall();
                CallApi.GetStockData(apiCall);
            }
            return Redirect("Index");
        }

        // GET: WorldTradeStockCurrents/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorldTradeStockCurrent worldTradeStockCurrent = db.WorldTradeStockCurrents.Find(id);
            if (worldTradeStockCurrent == null)
            {
                return HttpNotFound();
            }
            return View(worldTradeStockCurrent);
        }

        // GET: WorldTradeStockCurrents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorldTradeStockCurrents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Symbol,Price,Change,ChangePercent")] WorldTradeStockCurrent worldTradeStockCurrent)
        {
            if (ModelState.IsValid)
            {
                db.WorldTradeStockCurrents.Add(worldTradeStockCurrent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(worldTradeStockCurrent);
        }

        // GET: WorldTradeStockCurrents/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorldTradeStockCurrent worldTradeStockCurrent = db.WorldTradeStockCurrents.Find(id);
            if (worldTradeStockCurrent == null)
            {
                return HttpNotFound();
            }
            return View(worldTradeStockCurrent);
        }

        // POST: WorldTradeStockCurrents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Symbol,Price,Change,ChangePercent")] WorldTradeStockCurrent worldTradeStockCurrent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worldTradeStockCurrent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(worldTradeStockCurrent);
        }

        // GET: WorldTradeStockCurrents/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorldTradeStockCurrent worldTradeStockCurrent = db.WorldTradeStockCurrents.Find(id);
            if (worldTradeStockCurrent == null)
            {
                return HttpNotFound();
            }
            return View(worldTradeStockCurrent);
        }

        // POST: WorldTradeStockCurrents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WorldTradeStockCurrent worldTradeStockCurrent = db.WorldTradeStockCurrents.Find(id);
            db.WorldTradeStockCurrents.Remove(worldTradeStockCurrent);
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
