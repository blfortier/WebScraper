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

            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolios");
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
            Scrape_DisplayStockData(driver);

        }

        public static void LogIn(ChromeDriver webScraper)
        {
            webScraper.Navigate().GoToUrl("https://login.yahoo.com/");
            webScraper.FindElement(By.Id("login-username")).SendKeys("webdriverproj" + Keys.Enter);

            webScraper.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            webScraper.FindElement(By.Id("login-passwd")).SendKeys("Monkeys123" + Keys.Enter);
        }

        public static void Scrape_DisplayStockData(ChromeDriver webScraper)
        {
            webScraper.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IList<IWebElement> stockData = webScraper.FindElements(By.ClassName("simpTblRow"));
            Console.WriteLine("Total stocks: " + stockData.Count);

            for (int j = 0; j < stockData.Count; j++)
                Console.WriteLine(stockData[j].Text);

            IList<IWebElement> symbol_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Symbol']"));
            IList<IWebElement> lastPrice_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Last Price']"));
            IList<IWebElement> change_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Change']"));
            IList<IWebElement> changePercent_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Chg %']"));
            IList<IWebElement> marketTime_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Market Time']"));
            IList<IWebElement> volume_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Volume']"));
            IList<IWebElement> avgVolume_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Avg Vol (3m)']"));
            IList<IWebElement> shares_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Shares']"));
            IList<IWebElement> marketCap_elements = webScraper.FindElements(By.XPath("//*[@aria-label='Market Cap']"));

            string[] symbols = new string[symbol_elements.Count];
            double[] lastPrice = new double[lastPrice_elements.Count];
            double[] change = new double[change_elements.Count];
            double[] changePercent = new double[changePercent_elements.Count];
            string[] marketTime = new string[marketTime_elements.Count];
            string[] volume = new string[volume_elements.Count];
            int[] shares = new int[shares_elements.Count];
            string[] avgVolume = new string[avgVolume_elements.Count];
            string[] marketCap = new string[marketCap_elements.Count];

            Stock stock = new Stock();

            for (int i = 0; i < symbols.Length; i++)
            {
                symbols[i] = Convert.ToString(symbol_elements[i].Text);
                Console.WriteLine("Parsed: {0} + {1}", symbols[i], symbols[i].GetType());

                lastPrice[i] = Convert.ToDouble(lastPrice_elements[i].Text);
                Console.WriteLine("Parsed: {0} + {1}", lastPrice[i], lastPrice[i].GetType());

                change[i] = Convert.ToDouble(change_elements[i].Text);
                Console.WriteLine("Parsed: {0} + {1}", change[i], change[i].GetType());

                char trim = '%';
                changePercent[i] = Convert.ToDouble(changePercent_elements[i].Text.TrimEnd(trim));
                Console.WriteLine("Parsed: {0}% + {1}", changePercent[i], changePercent[i].GetType());

                marketTime[i] = Convert.ToString(marketTime_elements[i].Text);
                Console.WriteLine("Parsed: {0} + {1}", marketTime[i], marketTime[i].GetType());

                volume[i] = Convert.ToString(volume_elements[i].Text);
                Console.WriteLine("Parsed: {0}M + {1}", volume[i], volume[i].GetType());

                avgVolume[i] = Convert.ToString(avgVolume_elements[i].Text);
                Console.WriteLine("Parsed: {0}M + {1}", avgVolume[i], avgVolume[i].GetType());

                shares[i] = 0;
                Console.WriteLine("Parsed: {0} + {1}", shares[i], shares[i].GetType());

                marketCap[i] = Convert.ToString(marketCap_elements[i].Text);
                Console.WriteLine("Parsed: {0}B + {1}", marketCap[i], marketCap[i].GetType());


                stock = new Stock(symbols[i],
                                  lastPrice[i],
                                  change[i],
                                  changePercent[i],
                                  marketTime[i],
                                  volume[i],
                                  avgVolume[i],
                                  shares[i],
                                  marketCap[i]);

                Console.WriteLine("stock created");

                InsertStockDataIntoDatabase(stock, i);
            }
        }

        private static void InsertStockDataIntoDatabase(Stock stock, int i)
        {
            string connectionString = null;
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";

            string sql = "INSERT INTO StockInfo VALUES (@Id, @Symbol, @LastPrice, @Change, @ChangePercent, @MarketTime, @Volume, @AvgVol, @Shares, @MarketCap)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connection open...");

                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@Symbol", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@LastPrice", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@Change", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@MarketTime", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@Volume", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@AvgVol", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@Shares", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@MarketCap", SqlDbType.VarChar));

                        command.Parameters["@Id"].Value = i + 1;
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
                        Console.WriteLine("{0} added...", stock.Symbol);
                    }

                }

            }
        }       
    }
}
