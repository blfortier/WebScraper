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
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToAction("Register", "Account");

            return View(db.StockHistories.ToList());
        }

        [Authorize]
        public ActionResult ClearStockHistory()
        {
            if (ModelState.IsValid)
            {
                string connectionString = null;
                connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";

                Database.DeleteTableData(connectionString);
                Database.ResetAutoIncrementer(connectionString);                
            }
           // return RedirectToAction("Current Data", "Index", "Stocks");
            return RedirectToAction("Index");
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
