using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://login.yahoo.com/");
            driver.FindElement(By.Id("login-username")).SendKeys("webdriverproj" + Keys.Enter);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.Id("login-passwd")).SendKeys("Monkeys123" + Keys.Enter);

            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolios");
            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
                     
            IList<IWebElement> stockData = driver.FindElements(By.ClassName("simpTblRow"));
            IList<IWebElement> tableHeaders = driver.FindElements(By.XPath("//*[@id='pf - detail - table']/div[1]/table/thead/tr"));
            Console.WriteLine("Total stocks: " + stockData.Count);

            //for(int i = 0; i < tableHeaders.Count; i++)
            //{
            //    Console.WriteLine(tableHeaders[i].Text);              
            //}

            for (int j = 0; j < stockData.Count; j++)
                Console.WriteLine(stockData[j].Text);

        }
    }
}
