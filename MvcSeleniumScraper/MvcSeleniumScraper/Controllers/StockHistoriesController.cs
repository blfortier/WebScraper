using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Windows.Forms;
using MvcSeleniumScraper.Models;
using MvcSeleniumScraper.ScraperService;
using Database = MvcSeleniumScraper.ScraperService.Database;

namespace MvcSeleniumScraper.Controllers
{
    [Authorize]
    public class StockHistoriesController : Controller
    {
        private StockDataEntities2 db = new StockDataEntities2();

        // GET: StockHistories
        [Authorize]
        public ActionResult Index()
        {
            return View(db.StockHistories.ToList());
        }

        [Authorize]
        public ActionResult ClearStockHistory()
        {
            if (ModelState.IsValid)
            {                
                ScraperService.Database.Clear_Reset();
            }
           return Redirect("/Stocks");
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
