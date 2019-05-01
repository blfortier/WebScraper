using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class ApiCall
    {
        public List<string> Stocks;
        public string Url;
        public string Key;

        public ApiCall()
        {
            this.Stocks = new List<string>() { "HAS", "TWTR", "USMV",
                   "MINI", "KSS"};
            this.Url = "https://www.worldtradingdata.com/api/v1/stock";
            this.Key = "Jq0GZBASNat6TMVl2pZ5gzTSi2pSKLR8fYZYZK2kZblOdp7W3BBhsMDCinFQ";
        }  
    }
}
