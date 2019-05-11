﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MvcSeleniumScraper.ScraperService
{
    public class Scrape : Database
    {
        private string _userId;
        private string _password;
        public ChromeDriver driver;

        public string UserId { get => _userId; set => _userId = value; }
        public string Password { get => _password; set => _password = value; }

        public Scrape(string id, string pass)
        {
            this.UserId = id;
            this.Password = pass;

            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--headless");

            this.driver = new ChromeDriver(option);
        }

        public void NavigateToYahooFinance()
        {
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolios");
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
        }

        public void LogIn()
        {
            driver.Navigate().GoToUrl("https://login.yahoo.com/");
            driver.FindElement(By.Id("login-username")).SendKeys(this.UserId + Keys.Enter);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.Id("login-passwd")).SendKeys(this.Password + Keys.Enter);
        }

        public void ScrapeStockData()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IList<IWebElement> stockData = driver.FindElements(By.ClassName("simpTblRow"));
            Console.WriteLine("Total stocks: " + stockData.Count);

            IList<IWebElement> symbol_elements = driver.FindElements(By.XPath("//*[@aria-label='Symbol']"));
            IList<IWebElement> lastPrice_elements = driver.FindElements(By.XPath("//*[@aria-label='Last Price']"));
            IList<IWebElement> change_elements = driver.FindElements(By.XPath("//*[@aria-label='Change']"));
            IList<IWebElement> changePercent_elements = driver.FindElements(By.XPath("//*[@aria-label='Chg %']"));
            IList<IWebElement> volume_elements = driver.FindElements(By.XPath("//*[@aria-label='Volume']"));
            IList<IWebElement> avgVolume_elements = driver.FindElements(By.XPath("//*[@aria-label='Avg Vol (3m)']"));
            IList<IWebElement> marketCap_elements = driver.FindElements(By.XPath("//*[@aria-label='Market Cap']"));

            ScrapedData scrape = new ScrapedData(symbol_elements, lastPrice_elements, change_elements, changePercent_elements,
                                       volume_elements, avgVolume_elements, marketCap_elements);

            ParseScrapedData(scrape);
        }

        private static void ParseScrapedData(ScrapedData extractedData)
        {
            int stockTotal = extractedData.StockSymbols.Count;
            Console.WriteLine("stocktotal {0}", stockTotal);

            List<string> symbols = new List<string>();
            List<double> lastPrice = new List<double>();
            List<double> change = new List<double>();
            List<double> changePercent = new List<double>();
            List<string> volume = new List<string>();
            List<string> avgVolume = new List<string>();
            List<string> marketCap = new List<string>();

            Stocks stock = new Stocks();

            for (int i = 0; i < stockTotal; i++)
            {
                symbols.Insert(i, Convert.ToString(extractedData.StockSymbols[i].Text));
                lastPrice.Insert(i, Convert.ToDouble(extractedData.StockLastPrices[i].Text));
                change.Insert(i, Convert.ToDouble(extractedData.StockChanges[i].Text));

                char trim = '%';
                changePercent.Insert(i, Convert.ToDouble(extractedData.StockChangePercents[i].Text.TrimEnd(trim)));

                volume.Insert(i, Convert.ToString(extractedData.StockVolumes[i].Text));
                avgVolume.Insert(i, Convert.ToString(extractedData.StockAvgVolumes[i].Text));
                marketCap.Insert(i, Convert.ToString(extractedData.StockMarketCaps[i].Text));

                stock = new Stocks(symbols[i],
                                  lastPrice[i],
                                  change[i],
                                  changePercent[i],
                                  volume[i],
                                  avgVolume[i],
                                  marketCap[i]);

                InsertStockDataIntoDB(stock);
            }
        }
    }
}