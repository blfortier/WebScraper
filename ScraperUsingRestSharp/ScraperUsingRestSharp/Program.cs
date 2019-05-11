using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiCall = new ApiCallData();
            CallApi.GetStockData(apiCall);
        }
    }
}


   