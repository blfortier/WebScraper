using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeDriver driver = new ChromeDriver();
            // driver.Url = "https://finance.yahoo.com/portfolios";
            driver.Manage().Window.Maximize();
             
            driver.Navigate().GoToUrl("https://login.yahoo.com/");
            driver.FindElement(By.Id("login-username")).SendKeys("webdriverproj");
            driver.FindElement(By.Id("login-passwd")).SendKeys("Monkeys123");
            driver.FindElement(By.Id("login-signin")).Click();











            //driver.Manage().Timeouts().ImplicitWait(TimeSpan.FromSeconds(10));
            //driver.FindElement(By.XPath("//*[@data-reactid='15']")).Click();
            //driver.FindElement(By.Id("login-username")).SendKeys("webdriverproj");
            //driver.FindElement(By.Id("login-passwd")).SendKeys("Monkeys123" + Keys.Enter);



            //driver.Navigate().GoToUrl("https://finance.yahoo.com");


        }
    }
}
