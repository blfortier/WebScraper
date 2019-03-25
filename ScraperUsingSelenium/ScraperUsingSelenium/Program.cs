using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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

            ConnectToStockDataBase();
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
            int i = 0;
            int k = 0;

            foreach (var item in symbol_elements)
            {
                Console.WriteLine(item.Text);
                symbols[i++] = Convert.ToString(item.Text);
            }
            // Console.WriteLine("last price length: {0}", lastPrice.Length);
            foreach (var item in symbols)
            {
                Console.WriteLine("Parsed: {0} + {1}", item, item.GetType());
            }

            foreach (var item in lastPrice_elements)
            {
                Console.WriteLine(item.Text);
                lastPrice[k++] = Convert.ToDouble(item.Text);
            }

            foreach (var item in lastPrice)
            {
                Console.WriteLine("Parsed: {0} + {1}", item, item.GetType());
            }



            Console.WriteLine();

            foreach (var item in lastPrice_elements)
                Console.WriteLine(item.Text);

            Console.WriteLine();
            foreach (var item in change_elements)
                Console.WriteLine(item.Text);

            Console.WriteLine();
            foreach (var item in changePercent_elements)
                Console.WriteLine(item.Text);

            Console.WriteLine();
            foreach (var item in marketTime_elements)
                Console.WriteLine(item.Text);

            Console.WriteLine();
            foreach (var item in volume_elements)
                Console.WriteLine(item.Text);

            Console.WriteLine();
            foreach (var item in avgVolume_elements)
                Console.WriteLine(item.Text);

            Console.WriteLine();
            foreach (var item in shares_elements)
                Console.WriteLine(item.Text);

            Console.WriteLine();
            foreach (var item in marketCap_elements)
                Console.WriteLine(item.Text);

        }

        public static void ConnectToStockDataBase()
        {
            string connectionString = null;
            SqlConnection connect;
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";
            connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();
                Console.WriteLine("Connection open!");
                // connect.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot open connection...");
            }
        }
    }
}
