using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yelp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://api.yelp.com/v3/businesses/search");

            var request = new RestRequest("?term={restaurantName}");
        }
    }
}
