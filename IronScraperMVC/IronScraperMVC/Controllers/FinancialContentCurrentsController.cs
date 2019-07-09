using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IronScraperMVC.Models;
using IronScraperMVC.ScraperService;

namespace IronScraperMVC.Controllers
{
    public class FinancialContentCurrentsController : Controller
    {
        private StockDataEntities db = new StockDataEntities();

        // GET: FinancialContentCurrents
        public ActionResult Index()
        {
            return View(db.FinancialContentCurrents.ToList());
        }

        public ActionResult NewScrape()
        {
            if (ModelState.IsValid)
            {
                var scraper = new Scraper();
                scraper.Start();
                scraper.AddStockToDatabase();
            }
            return Redirect("Index");
        }

        // GET: FinancialContentCurrents/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialContentCurrent financialContentCurrent = db.FinancialContentCurrents.Find(id);
            if (financialContentCurrent == null)
            {
                return HttpNotFound();
            }
            return View(financialContentCurrent);
        }
    }
}
