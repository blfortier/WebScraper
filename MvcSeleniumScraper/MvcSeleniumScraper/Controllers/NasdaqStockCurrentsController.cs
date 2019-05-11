using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MvcSeleniumScraper.HAPScraperService;
using MvcSeleniumScraper.Models;

namespace MvcSeleniumScraper.Controllers
{
    [Authorize]
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

        [Authorize]
        public ActionResult NewScrape()
        {
            if (ModelState.IsValid)
            {
                string nasdaqWebsite = "https://www.nasdaq.com/markets/most-active.aspx";

                HapScrape scrape = new HapScrape(nasdaqWebsite);
                scrape.ScrapeStockData();

            }

            return Redirect("Index");
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
