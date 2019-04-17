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
    public class StockHistoriesController : Controller
    {
        private StockDataEntities db = new StockDataEntities();

        // GET: StockHistories
        [Authorize]
        public ActionResult Index()
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToAction("Register", "Account");

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
