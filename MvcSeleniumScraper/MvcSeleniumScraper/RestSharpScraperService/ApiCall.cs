using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.RestSharpScraperService
{
    public class ApiCall
    {
        public List<string> Stocks;
        public List<ApiCallResponse> StockList;
        public string Url;
        public string Key;

        public ApiCall()
        {
            this.Stocks = new List<string>() { "HAS", "TWTR", "USMV",
                   "MINI", "KSS"};
            this.StockList = new List<ApiCallResponse>();

            this.Url = "https://www.worldtradingdata.com/api/v1/stock";
            this.Key = "Jq0GZBASNat6TMVl2pZ5gzTSi2pSKLR8fYZYZK2kZblOdp7W3BBhsMDCinFQ";
        }
    }
}