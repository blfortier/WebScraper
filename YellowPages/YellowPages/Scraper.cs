using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowPages
{
    public class Scraper
    {
        private string _searchFor = "restaurant";

        private ChromeDriver _driver;
        public string SearchFor { get => _searchFor; set => _searchFor = value; }
        public ChromeDriver Driver { get => _driver; set => _driver = value; }

        public Scraper()
        {
            // ChromeOptions headless = new ChromeOptions();
            // headless.AddArgument("--headless");

            this.Driver = new ChromeDriver();
        } 

        public void NavigateToYellowPagesSearchBox()
        {

            try
            {
                Driver.Navigate().GoToUrl("https://www.yellowpages.com");
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Driver.FindElementByXPath("//*[@id='query']").SendKeys("restaurant");
                Driver.FindElementByXPath("//*[@id='search-form']/button").Click();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            finally
            {
               //Driver.Close();
            }
        }

        public void GetBusinessData()
        {
            IList<IWebElement> data = Driver.FindElements(By.XPath("//*/div/div/div/h2"));

            IList<IWebElement> name_elements = Driver.FindElements(By.XPath("//div/div/div[2]/h2/a/span"));
            IList<IWebElement> address_elements = Driver.FindElements(By.XPath("//*[@class='street-address']"));
            IList<IWebElement> city_elements = Driver.FindElements(By.XPath("//*[@class='locality']"));

            Console.WriteLine(name_elements.Count);
          
            for (int i = 0; i < name_elements.Count; i++)
            {
                Console.WriteLine("{0} : {1}, {2}", name_elements[i].Text, address_elements[i].Text, city_elements[i].Text);
            }
        }
    }
}
