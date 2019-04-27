using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ScraperUsingSelenium
{
    class Program
    {
        static void Main(string[] args)
        {
            var scrape = new Scrape("webdriverproj", "Monkeys123");

            scrape.LogIn();
            scrape.NavigateToYahooFinance();
            scrape.ScrapeStockData();
        }      
    }
}
    

