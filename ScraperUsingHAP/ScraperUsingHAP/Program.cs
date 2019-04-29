using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ScraperUsingHAP
{
    class Program
    { 
        static void Main(string[] args)
        {
            string nasdaqWebsite = "https://www.nasdaq.com/markets/most-active.aspx";

            HapScrape scrape = new HapScrape(nasdaqWebsite);
            scrape.ScrapeStockData();
        }
    }
}
