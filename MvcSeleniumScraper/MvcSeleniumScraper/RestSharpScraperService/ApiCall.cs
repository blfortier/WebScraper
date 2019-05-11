using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.RestSharpScraperService
{
    public class ApiCall
    {
        private List<string> _stocks;
        private List<ApiCallResponse> stockList;

        public List<string> Stocks { get => _stocks; set => _stocks = value; }
        public List<ApiCallResponse> StockList { get => stockList; set => stockList = value; }

        public ApiCall()
        {
            this.Stocks = new List<string>() { "HAS", "TWTR", "USMV",
                   "MINI", "KSS"};
            this.StockList = new List<ApiCallResponse>();
        }
    }
}