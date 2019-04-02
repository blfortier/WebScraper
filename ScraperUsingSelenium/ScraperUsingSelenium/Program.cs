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
            ChromeDriver driver = new ChromeDriver();

            LogIn(driver);
            NavigateToYahooFinance(driver);
            ScrapeStockData(driver);
        }

        private static void NavigateToYahooFinance(ChromeDriver driver)
        {
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolios");
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
        }

        public static void LogIn(ChromeDriver webScraper)
        {
            webScraper.Navigate().GoToUrl("https://login.yahoo.com/");
            webScraper.FindElement(By.Id("login-username")).SendKeys("webdriverproj" + Keys.Enter);

            webScraper.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            webScraper.FindElement(By.Id("login-passwd")).SendKeys("Monkeys123" + Keys.Enter);
        }

        public static void ScrapeStockData(ChromeDriver webScraper)
        {
            webScraper.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IList<IWebElement> stockData = webScraper.FindElements(By.ClassName("simpTblRow"));
            Console.WriteLine("Total stocks: " + stockData.Count);

            //for (int j = 0; j < stockData.Count; j++)
            //    Console.WriteLine(stockData[j].Text);

            IList<IWebElement> symbol_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Symbol']"));
            IList<IWebElement> lastPrice_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Last Price']"));
            IList<IWebElement> change_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Change']"));
            IList<IWebElement> changePercent_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Chg %']"));
            IList<IWebElement> marketTime_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Market Time']"));
            IList<IWebElement> volume_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Volume']"));
            IList<IWebElement> avgVolume_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Avg Vol (3m)']"));
            IList<IWebElement> shares_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Shares']"));
            IList<IWebElement> marketCap_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Market Cap']"));

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

            Stock stock = new Stock();

            /*
             Z	35.29	0.55	1.58	11:53AM EDT	417.784k	3.021M	-	7.188B
            AAPL	190.13	0.18	0.09	11:54AM EDT	13.274M	31.993M	-	896.846B
            MSFT	118.73	0.79	0.67	11:54AM EDT	7.101M	29.056M	-	911.228B
            CVS	53.85	-0.08	-0.16	11:54AM EDT	3.711M	13.427M	-	69.802B
            BJ	27.8	0.4	1.44	11:54AM EDT	574.208k	1.774M	-	3.832B
            HD	193.39	1.5	0.78	11:54AM EDT	2.063M	4.657M	-	213.384B
            BBBY	16.73	-0.26	-1.56	11:54AM EDT	3.148M	7.837M	-	2.297B
            AMZN	1804.92	24.17	1.36	11:54AM EDT	2.068M	5.177M	-	886.722B
            ATVI	46.42	0.89	1.95	11:54AM EDT	2.923M	13.326M	-	35.45B
            EA	102.95	1.32	1.3	11:54AM EDT	1.154M	7.748M	-	30.851B                      
             */

            for (int i = 0; i < stockTotal; i++)
            {
                symbols[i] = Convert.ToString(extractedData.StockSymbols[i].Text);
              //  Console.WriteLine("Parsed: {0} + {1}", symbols[i], symbols[i].GetType());

                lastPrice[i] = Convert.ToDouble(extractedData.StockLastPrices[i].Text);
             //   Console.WriteLine("Parsed: {0} + {1}", lastPrice[i], lastPrice[i].GetType());

                change[i] = Convert.ToDouble(extractedData.StockChanges[i].Text);
             //   Console.WriteLine("Parsed: {0} + {1}", change[i], change[i].GetType());

                char trim = '%';
                changePercent[i] = Convert.ToDouble(extractedData.StockChangePercents[i].Text.TrimEnd(trim));
             //   Console.WriteLine("Parsed: {0}% + {1}", changePercent[i], changePercent[i].GetType());

                marketTime[i] = Convert.ToString(extractedData.StockMarketTimes[i].Text);
             //   Console.WriteLine("Parsed: {0} + {1}", marketTime[i], marketTime[i].GetType());

                volume[i] = Convert.ToString(extractedData.StockVolumes[i].Text);
             //   Console.WriteLine("Parsed: {0}M + {1}", volume[i], volume[i].GetType());

                avgVolume[i] = Convert.ToString(extractedData.StockAvgVolumes[i].Text);
             //   Console.WriteLine("Parsed: {0}M + {1}", avgVolume[i], avgVolume[i].GetType());

                shares[i] = Convert.ToString(extractedData.StockShares[i].Text);
            //    Console.WriteLine("Parsed: {0} + {1}", shares[i], shares[i].GetType());

                marketCap[i] = Convert.ToString(extractedData.StockMarketCaps[i].Text);
            //    Console.WriteLine("Parsed: {0}B + {1}", marketCap[i], marketCap[i].GetType());


                stock = new Stock(symbols[i],
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

        private static void InsertStockDataIntoDatabase(Stock stock)
        {
            string connectionString = null;
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";

            //for(int i = 0; i < 2; i++)
            //{
            //    DeleteTableData(connectionString);
            //    ResetAutoIncrementer(connectionString);
            //}            

         //   InsertIntoLatestSrape(stock, connectionString);
            InsertIntoSrapeHistory(stock, connectionString);
        }

        private static void InsertIntoLatestSrape(Stock stock, string connectionString)
        {
            string latestScrape = "INSERT INTO LatestScrape VALUES (@Symbol, @LastPrice, @Change, @ChangePercent, @MarketTime, @Volume, @AvgVol, @Shares, @MarketCap);";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                   // Console.WriteLine("Connection open...");

                    using (SqlCommand command = new SqlCommand(latestScrape, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Symbol", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@LastPrice", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@Change", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@MarketTime", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@Volume", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@AvgVol", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@Shares", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@MarketCap", SqlDbType.VarChar));

                        command.Parameters["@Symbol"].Value = stock.Symbol;
                        command.Parameters["@LastPrice"].Value = stock.LastPrice;
                        command.Parameters["@Change"].Value = stock.Change;
                        command.Parameters["@ChangePercent"].Value = stock.ChangePercent;
                        command.Parameters["@MarketTime"].Value = stock.MarketTime;
                        command.Parameters["@Volume"].Value = stock.Volume;
                        command.Parameters["@AvgVol"].Value = stock.AvgVol;
                        command.Parameters["@Shares"].Value = stock.Shares;
                        command.Parameters["@MarketCap"].Value = stock.MarketCap;

                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to LatestScrape table...", stock.Symbol);
                        // DeleteTableData(con);
                        //DeleteTableData(con);
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                //con.Close();
                //if (con.State == System.Data.ConnectionState.Closed)
                //    Console.WriteLine("Connection sucessfully closed...");
            }
        }

        private static void InsertIntoSrapeHistory(Stock stock, string connectionString)
        {
            string scrapeHistory = "INSERT INTO ScrapeHistory VALUES (@Symbol, @LastPrice, @Change, @ChangePercent, @MarketTime, @Volume, @AvgVol, @Shares, @MarketCap);";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                  //  Console.WriteLine("Connection open...");

                    using (SqlCommand command = new SqlCommand(scrapeHistory, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Symbol", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@LastPrice", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@Change", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@MarketTime", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@Volume", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@AvgVol", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@Shares", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@MarketCap", SqlDbType.VarChar));

                        command.Parameters["@Symbol"].Value = stock.Symbol;
                        command.Parameters["@LastPrice"].Value = stock.LastPrice;
                        command.Parameters["@Change"].Value = stock.Change;
                        command.Parameters["@ChangePercent"].Value = stock.ChangePercent;
                        command.Parameters["@MarketTime"].Value = stock.MarketTime;
                        command.Parameters["@Volume"].Value = stock.Volume;
                        command.Parameters["@AvgVol"].Value = stock.AvgVol;
                        command.Parameters["@Shares"].Value = stock.Shares;
                        command.Parameters["@MarketCap"].Value = stock.MarketCap;

                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to ScrapeHistory table...", stock.Symbol);
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                //con.Close();
                //if (con.State == System.Data.ConnectionState.Closed)
                //    Console.WriteLine("Connection sucessfully closed...");
            }
        }

        private static void DeleteTableData(string connection)
        {
            string deleteTableData = "DELETE FROM LatestScrape;";
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = new SqlCommand(deleteTableData, con))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Table cleared...");
                    }
                }

            }
        }

        private static void ResetAutoIncrementer(string connection)
        {
            string reseed = "DBCC CHECKIDENT ('LatestScrape', RESEED, 0);";

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = new SqlCommand(reseed, con))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Auto incrementer reset...");
                    }
                }
            }
        }
    }
}
    

