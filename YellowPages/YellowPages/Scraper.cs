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
            //     options.addArguments("userdir=C:/Users/[your user name]/AppData/Local/Google/Chrome/User Data");

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
              //  Driver.Close();
            }
        }

        public void GetBusinessData()
        {
            // //*[@id="lid-946496"]/div/div[2]/div[2]/h2/a/span
            for (int numOfBusinesses = 0; numOfBusinesses < 10; numOfBusinesses++)
            {
                IList<IWebElement> data = Driver.FindElements(By.XPath("//*/div/div/div/h2"));

                IList<IWebElement> name_elements = Driver.FindElements(By.XPath("//*[@class='business-name']"));

                foreach (var item in name_elements)
                {
                    Console.WriteLine(item.Inn
                        );
                }
            }
            // //*[@class='business-name']"
        }
    }
}
