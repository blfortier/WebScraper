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
    public class NasdaqStockHistoriesController : Controller
    {
        private StockDataEntities1 db = new StockDataEntities1();

        // GET: NasdaqStockHistories
        public ActionResult Index()
        {
            return View(db.NasdaqStockHistories.ToList());
        }

        [Authorize]
        public ActionResult ClearStockHistory()
        {
            if (ModelState.IsValid)
            {
                HAPScraperService.Database.Clear_Reset();
            }
  
            return Redirect("/NasdaqStockCurrents");
        }
        // GET: NasdaqStockHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NasdaqStockHistory nasdaqStockHistory = db.NasdaqStockHistories.Find(id);
            if (nasdaqStockHistory == null)
            {
                return HttpNotFound();
            }
            return View(nasdaqStockHistory);
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
