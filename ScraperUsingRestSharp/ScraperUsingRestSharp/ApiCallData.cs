using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class ApiCallData
    {
        private List<string> stocks;
        private List<ApiCallResponse> stockList;
       
        public List<string> Stocks { get => stocks; set => stocks = value; }
        internal List<ApiCallResponse> StockList { get => stockList; set => stockList = value; }

        public ApiCallData()
        {
            this.Stocks = new List<string>() { "HAS", "TWTR", "USMV",
                   "MINI", "KSS"};
            this.StockList = new List<ApiCallResponse>();
        }
    }
}
