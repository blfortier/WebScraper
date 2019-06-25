using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingIron
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new StockScraper();
            scraper.Start();
            scraper.AddStockToDatabase();
        }
    }
}
