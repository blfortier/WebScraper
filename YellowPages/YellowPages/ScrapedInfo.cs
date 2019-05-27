using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowPages
{
    public class ScrapedInfo
    {
        private IList<IWebElement> _restaurantName;
        private IList<IWebElement> _restaurantAddress;
        private IList<IWebElement> _restaurantCity;
        private IList<IWebElement> _restaurantState;
        private IList<IWebElement> _restaurantZip;
        private IList<IWebElement> _restaurantPhoneNumber;

        
        public IList<IWebElement> RestaurantName { get => _restaurantName; set => _restaurantName = value; }
        public IList<IWebElement> RestaurantAddress { get => _restaurantAddress; set => _restaurantAddress = value; }
        public IList<IWebElement> RestaurantCity { get => _restaurantCity; set => _restaurantCity = value; }
        public IList<IWebElement> RestaurantState { get => _restaurantState; set => _restaurantState = value; }
        public IList<IWebElement> RestaurantZip { get => _restaurantZip; set => _restaurantZip = value; }
        public IList<IWebElement> RestaurantPhoneNumber { get => _restaurantPhoneNumber; set => _restaurantPhoneNumber = value; }

        public ScrapedInfo(IList<IWebElement> restaurantName, IList<IWebElement> restaurantAddress, IList<IWebElement> restaurantCity, IList<IWebElement> restaurantState, IList<IWebElement> restaurantZip, IList<IWebElement> restaurantPhoneNumber)
        {
            RestaurantName = restaurantName;
            RestaurantAddress = restaurantAddress;
            RestaurantCity = restaurantCity;
            RestaurantState = restaurantState;
            RestaurantZip = restaurantZip;
            RestaurantPhoneNumber = restaurantPhoneNumber;
        }

    }
}
