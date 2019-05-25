using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowPages
{
    class Program
    {
        static void Main(string[] args)
        {
            Scraper searchYP = new Scraper();
            searchYP.NavigateToYellowPagesSearchBox();
            searchYP.GetBusinessData();

        //    Console.WriteLine(searchYP.ListOfRestaurants);
            //foreach (var item in searchYP.ListOfRestaurants)
            //{
            //    Console.WriteLine("Name: {0}\n" +
            //                      "Address: {1}\n" +
            //                      "City & State: {2}\n" +
            //                      "Phone Number: {3}\n", item.Name, item.Address, item.CityState, item.PhoneNumber);
            //}
        }
    }
}
