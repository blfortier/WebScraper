using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScraperUsingSelenium
{
    class Scrape : Database
    {
        private string _userId;
        private string _password;
        public ChromeDriver driver;

        public Scrape(string id, string pass)
        {
            this._userId = id;
            this._password = pass;

           // ChromeOptions option = new ChromeOptions();
          //  option.AddArgument("--headless");

            this.driver = new ChromeDriver();
        }

        public void NavigateToYahooFinance()
        {
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolios");
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
        }

        public void LogIn()
        {
            driver.Navigate().GoToUrl("https://login.yahoo.com/");
            driver.FindElement(By.Id("login-username")).SendKeys(this._userId + Keys.Enter);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.Id("login-passwd")).SendKeys(this._password + Keys.Enter);
        }

        public void ScrapeStockData()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IList<IWebElement> stockData = driver.FindElements(By.ClassName("simpTblRow"));
            Console.WriteLine("Total stocks: " + stockData.Count);

            // driver.FindElement(By.CssSelector("button[data-action-value]")).GetAttribute("data-action-value");

          //  IList<IWebElement> name_elements = driver.FindElements(By.CssSelector("//*[@aria-label='Symbol']//a"));
            IList<IWebElement> symbol_elements = driver.FindElements(By.XPath("//*[@aria-label='Symbol']"));
            IList<IWebElement> lastPrice_elements = driver.FindElements(By.XPath("//*[@aria-label='Last Price']"));
            IList<IWebElement> changePercent_elements = driver.FindElements(By.XPath("//*[@aria-label='Chg %']"));
            IList<IWebElement> volume_elements = driver.FindElements(By.XPath("//*[@aria-label='Volume']"));
            IList<IWebElement> avgVolume_elements = driver.FindElements(By.XPath("//*[@aria-label='Avg Vol (3m)']"));
            IList<IWebElement> marketCap_elements = driver.FindElements(By.XPath("//*[@aria-label='Market Cap']"));

            ScrapedData scrape = new ScrapedData(symbol_elements, lastPrice_elements, changePercent_elements,
                                       volume_elements, avgVolume_elements, marketCap_elements);

            //foreach (var item in name_elements)
            //{
            //    Console.WriteLine(item.GetAttribute("title"));
            //}

            ParseScrapedData(scrape);
            driver.Close();
        }

        private static void ParseScrapedData(ScrapedData extractedData)
        {
            int stockTotal = extractedData.StockSymbols.Count;
            Console.WriteLine("stocktotal {0}", stockTotal);

            List<string> symbols = new List<string>();
            List<double> lastPrice = new List<double>();
            List<double> changePercent = new List<double>();
            List<string> volume = new List<string>();
            List<string> avgVolume = new List<string>();
            List<string> marketCap = new List<string>();

            Stock stock = new Stock();

            for (int i = 0; i < stockTotal; i++)
            {
                symbols.Insert(i, Convert.ToString(extractedData.StockSymbols[i].Text));
                lastPrice.Insert(i, Convert.ToDouble(extractedData.StockLastPrices[i].Text));

                char trim = '%';
                changePercent.Insert(i, Convert.ToDouble(extractedData.StockChangePercents[i].Text.TrimEnd(trim)));

                volume.Insert(i, Convert.ToString(extractedData.StockVolumes[i].Text));
                avgVolume.Insert(i, Convert.ToString(extractedData.StockAvgVolumes[i].Text));
                marketCap.Insert(i, Convert.ToString(extractedData.StockMarketCaps[i].Text));

                stock = new Stock(symbols[i],
                                  lastPrice[i],
                                  changePercent[i],
                                  volume[i],
                                  avgVolume[i],
                                  marketCap[i]);

                Console.WriteLine("{0} stock created", symbols[i]);

                InsertStockHistory(stock);
                InsertCurrentStock(stock);
            }
        }
    }
}
