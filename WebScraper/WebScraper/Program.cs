﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace WebScraper
{
    partial class Program
    {
        static void Main(string[] args)
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            LogIn(driver);

            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolios");
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
            Scrape_DisplayStockData(driver);

            ConnectToStockDataBase();       
        }

        public class StockRepository
        {


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
            IList<IWebElement> tableHeaders = webScraper.FindElements(By.TagName("th"));
            Console.WriteLine("Total stocks: " + stockData.Count);
            Console.WriteLine("Total headers: " + tableHeaders.Count);

            //tableHeaders.RemoveAt(9);
            //tableHeaders.RemoveAt(10);
            //tableHeaders.RemoveAt(11);
            //tableHeaders.RemoveAt(13);

            Console.WriteLine("Total headers: " + tableHeaders.Count);

            for (int i = 0; i < tableHeaders.Count; i++)
            {
                Console.Write(" {0}", tableHeaders[i].Text);
            }

            for (int j = 0; j < stockData.Count; j++)
                Console.WriteLine(stockData[j].Text);
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
