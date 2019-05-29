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
            IList<IWebElement> city_elements = Driver.FindElements(By.XPath("//*[@class='locality']"));
            // //div/div/div[2]/div[2]/div[3]
            IList<IWebElement> state_elements = Driver.FindElements(By.XPath("//div/div/div[2]/div[1]/p/span[3]"));
            IList<IWebElement> zipCode_elements = Driver.FindElements(By.XPath("//div/div/div[2]/div[1]/p/span[4]"));
            IList<IWebElement> phoneNumber_elements = Driver.FindElements(By.XPath("//*[@class='phones phone primary']"));

            ScrapedInfo restaurantInfo = new ScrapedInfo(name_elements, address_elements, city_elements, phoneNumber_elements);
            ParseData(restaurantInfo);

            foreach (var item in city_elements)
                Console.WriteLine("city els: {0}", item.Text);
        }

        public static void ParseData(ScrapedInfo dataToParse)
        {
            int restaurantTotal = dataToParse.RestaurantName.Count;
            Console.WriteLine("There are {0} restaurants returned from YP", restaurantTotal);

            List<string> names = new List<string>();
            List<string> addresses = new List<string>();
            List<string> cities = new List<string>();
            List<string> locale = new List<string>();
            List<string> states = new List<string>();
            List<string> zips = new List<string>();
            List<string> phoneNumbers = new List<string>();

            Restaurant restaurant;

            for (int i = 0; i < restaurantTotal -1; i++)
            {
                names.Insert(i, Convert.ToString(dataToParse.RestaurantName[i].Text));
                Console.WriteLine("Parsed: {0} + {1}", names[i], names[i].GetType());

                addresses.Insert(i, Convert.ToString(dataToParse.RestaurantAddress[i].Text));
            //    Console.WriteLine("Parsed: {0} + {1}", addresses[i], addresses[i].GetType());
               // string city = Convert.ToString(dataToParse.RestaurantCity[i].Text);
            //    string state = Convert.ToString(dataToParse.RestaurantState[i].Text);
             //   string zip = Convert.ToString(dataToParse.RestaurantZip[i].Text);
             //   string concated = ConcatCityStateZip(restaurantTotal, city, state, zip);

                cities.Insert(i, Convert.ToString(dataToParse.RestaurantCity[i].Text));
            //    locale.Insert(i, concated);
         //       Console.WriteLine("Parsed: {0} + {1}", locale[i], locale[i].GetType());


                //    cities.Insert(i, Convert.ToString(dataToParse.RestaurantCity[i].Text));
                //    Console.WriteLine("Parsed: {0} + {1}", cities[i], cities[i].GetType());

                //     states.Insert(i, Convert.ToString(dataToParse.RestaurantState[i].Text));
                //    Console.WriteLine("Parsed: {0} + {1}", cities[i], cities[i].GetType());

                //       zips.Insert(i, Convert.ToString(dataToParse.RestaurantZip[i].Text));
                //      Console.WriteLine("Parsed: {0} + {1}", cities[i], cities[i].GetType());

                phoneNumbers.Insert(i, Convert.ToString(dataToParse.RestaurantPhoneNumber[i].Text));
                //                Console.WriteLine("Parsed: {0} + {1}", phoneNumbers[i], phoneNumbers[i].GetType());


            }
        }

        //public static Restaurant ConvertDataToRestaurantObject(List<string> names, List<string> address,
        //                                        List<string> city, List<string> phone)
        //{
        //    Restaurant restaurant;

        //    for (int i = 0; i < names.Count; i++)
        //    {
        //        restaurant.Name = names[i];
        //        restaurant.Address = address[i];
        //        restaurant.CityState = city[i];
        //        restaurant.PhoneNumber = phone[i];                                            
        //    }

        //    return restaurant;
        //}

        public static string ConcatCityStateZip(int total, string city, string state, string zip)
        {
            
            return (city + state + zip);
            
        }
    }
}
