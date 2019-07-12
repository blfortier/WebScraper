using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IronScraperMVC.Models;

namespace IronScraperMVC.Controllers
{
    public class FinancialContentHistoriesController : Controller
    {
        private StockDataEntities db = new StockDataEntities();

        // GET: FinancialContentHistories
        public ActionResult Index()
        {
            return View(db.FinancialContentHistories.ToList());
        }

        [Authorize]
        public ActionResult ClearStockHistory()
        {
            if (ModelState.IsValid)
            {
                ScraperService.Database.Clear_Reset();
            }
            return Redirect("/FinancialContentCurrents");
        }


        // GET: FinancialContentHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialContentHistory financialContentHistory = db.FinancialContentHistories.Find(id);
            if (financialContentHistory == null)
            {
                return HttpNotFound();
            }
            return View(financialContentHistory);
        }

    
    }
}
