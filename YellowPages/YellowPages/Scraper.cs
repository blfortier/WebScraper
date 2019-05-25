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
        private List<Restaurant> listOfRestaurants;

        private ChromeDriver _driver;
        public string SearchFor { get => _searchFor; set => _searchFor = value; }
        public ChromeDriver Driver { get => _driver; set => _driver = value; }
        public List<Restaurant> ListOfRestaurants { get => listOfRestaurants; set => listOfRestaurants = value; }

        public Scraper()
        {
            // ChromeOptions headless = new ChromeOptions();
            // headless.AddArgument("--headless");

            this.Driver = new ChromeDriver();
            this.ListOfRestaurants = new List<Restaurant>();
        } 

        public void NavigateToYellowPagesSearchBox()
        {

            try
            {
                Driver.Navigate().GoToUrl("https://www.yellowpages.com");
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Driver.FindElementByXPath("//*[@id='query']").SendKeys(_searchFor);
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
            IList<IWebElement> city_elements = Driver.FindElements(By.XPath("//*[@class='locality']")).ToList();
            IList<IWebElement> phoneNumber_elements = Driver.FindElements(By.XPath("//*[@class='phones phone primary']"));

            Console.WriteLine(name_elements.Count);

            //Console.WriteLine(city_elements);
            foreach (var item in city_elements)
            {
                Console.WriteLine(item.Text);
            }

            for (int i = 0; i < name_elements.Count; i++)
            {
                //   Console.WriteLine("{0} : {1}, {2} {3}", name_elements[i].Text,
                //                                      address_elements[i].Text, 
                //                                      city_elements[i].Text, 
                //                                      phoneNumber_elements[i].Text);

             

                var restaurant = new Restaurant(name_elements[i].Text,
                                                address_elements[i].Text,
                                                city_elements[i].Text,
                                                phoneNumber_elements[i].Text);
                ListOfRestaurants.Add(restaurant);
            }
        }
    }
}
