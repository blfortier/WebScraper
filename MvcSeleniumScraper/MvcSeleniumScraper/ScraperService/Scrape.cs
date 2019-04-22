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

        public Scrape(string id, string pass)
        {
            this._userId = id;
            this._password = pass;

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
            driver.FindElement(By.Id("login-username")).SendKeys(this._userId + Keys.Enter);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.Id("login-passwd")).SendKeys(this._password + Keys.Enter);
        }

        public void ScrapeStockData()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IList<IWebElement> stockData = driver.FindElements(By.ClassName("simpTblRow"));
            Console.WriteLine("Total stocks: " + stockData.Count);

            //for (int j = 0; j < stockData.Count; j++)
            //    Console.WriteLine(stockData[j].Text);

            IList<IWebElement> symbol_elements = driver.FindElements(By.XPath("//*[@aria-label='Symbol']"));
            IList<IWebElement> lastPrice_elements = driver.FindElements(By.XPath("//*[@aria-label='Last Price']"));
            IList<IWebElement> change_elements = driver.FindElements(By.XPath("//*[@aria-label='Change']"));
            IList<IWebElement> changePercent_elements = driver.FindElements(By.XPath("//*[@aria-label='Chg %']"));
            IList<IWebElement> marketTime_elements = driver.FindElements(By.XPath("//*[@aria-label='Market Time']"));
            IList<IWebElement> volume_elements = driver.FindElements(By.XPath("//*[@aria-label='Volume']"));
            IList<IWebElement> avgVolume_elements = driver.FindElements(By.XPath("//*[@aria-label='Avg Vol (3m)']"));
            IList<IWebElement> shares_elements = driver.FindElements(By.XPath("//*[@aria-label='Shares']"));
            IList<IWebElement> marketCap_elements = driver.FindElements(By.XPath("//*[@aria-label='Market Cap']"));

            ScrapedData scrape = new ScrapedData(symbol_elements, lastPrice_elements, change_elements, changePercent_elements,
                                        marketTime_elements, volume_elements, avgVolume_elements, shares_elements, marketCap_elements);

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
            List<string> marketTime = new List<string>();
            List<string> volume = new List<string>();
            List<string> shares = new List<string>();
            List<string> avgVolume = new List<string>();
            List<string> marketCap = new List<string>();

            Stocks stock = new Stocks();

            for (int i = 0; i < stockTotal; i++)
            {
                symbols.Insert(i, Convert.ToString(extractedData.StockSymbols[i].Text));
                //  Console.WriteLine("Parsed: {0} + {1}", symbols[i], symbols[i].GetType());

                lastPrice.Insert(i, Convert.ToDouble(extractedData.StockLastPrices[i].Text));
                //   Console.WriteLine("Parsed: {0} + {1}", lastPrice[i], lastPrice[i].GetType());

                change.Insert(i, Convert.ToDouble(extractedData.StockChanges[i].Text));
                //   Console.WriteLine("Parsed: {0} + {1}", change[i], change[i].GetType());

                char trim = '%';
                changePercent.Insert(i, Convert.ToDouble(extractedData.StockChangePercents[i].Text.TrimEnd(trim)));
                //   Console.WriteLine("Parsed: {0}% + {1}", changePercent[i], changePercent[i].GetType());

                marketTime.Insert(i, Convert.ToString(extractedData.StockMarketTimes[i].Text));
                //   Console.WriteLine("Parsed: {0} + {1}", marketTime[i], marketTime[i].GetType());

                volume.Insert(i, Convert.ToString(extractedData.StockVolumes[i].Text));
                //   Console.WriteLine("Parsed: {0}M + {1}", volume[i], volume[i].GetType());

                avgVolume.Insert(i, Convert.ToString(extractedData.StockAvgVolumes[i].Text));
                //   Console.WriteLine("Parsed: {0}M + {1}", avgVolume[i], avgVolume[i].GetType());

                shares.Insert(i, Convert.ToString(extractedData.StockShares[i].Text));
                //    Console.WriteLine("Parsed: {0} + {1}", shares[i], shares[i].GetType());

                marketCap.Insert(i, Convert.ToString(extractedData.StockMarketCaps[i].Text));
                //    Console.WriteLine("Parsed: {0}B + {1}", marketCap[i], marketCap[i].GetType());


                stock = new Stocks(symbols[i],
                                  lastPrice[i],
                                  change[i],
                                  changePercent[i],
                                  marketTime[i],
                                  volume[i],
                                  avgVolume[i],
                                  shares[i],
                                  marketCap[i]);

                Console.WriteLine("{0} stock created", symbols[i]);

                InsertStockDataIntoDatabase(stock);
            }
        }
    }
}