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
    [Authorize]
    public class WorldTradeStockHistoriesController : Controller
    {
        private StockDataEntities4 db = new StockDataEntities4();

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
            }
            return RedirectToAction("Index");


           // return View(worldTradeStockHistory);
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
