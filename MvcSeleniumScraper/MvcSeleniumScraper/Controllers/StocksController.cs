using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcSeleniumScraper.Models;
using MvcSeleniumScraper.ScraperService;

namespace MvcSeleniumScraper.Controllers
{
    [OverrideAuthorization]
    [Authorize]
    public class StocksController : Controller
    {
        private StockDataEntities2 db = new StockDataEntities2();

        // GET: Stocks
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Stocks.ToList());
        }

        [Authorize]
        public ActionResult NewScrape()
        {
            if (ModelState.IsValid)
            {
                var scrape = new Scrape("webdriverproj", "Monkeys123");

                scrape.LogIn();
                scrape.NavigateToYahooFinance();
                scrape.ScrapeStockData();
            }
            return RedirectToAction("Index");
        }

        // GET: Stocks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
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
