using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class ApiResponse
    {
        public string Symbol { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Price { get; set; }
        public string Volume { get; set; }
        public string LatestTradeDay { get; set; }
        public string PreviousClose { get; set; }
        public string Change { get; set; }
        public string ChangePercent { get; set; }

        public ApiResponse(Newtonsoft.Json.Linq.JObject res)
        {
            this.Symbol = res["Global Quote"]["01. symbol"].ToString();
            this.Open = res["Global Quote"]["02. open"].ToString();
            this.High = res["Global Quote"]["03. high"].ToString();
            this.Low = res["Global Quote"]["04. low"].ToString();
            this.Price = res["Global Quote"]["05. price"].ToString();
            this.Volume = res["Global Quote"]["06. volume"].ToString();
            this.LatestTradeDay = res["Global Quote"]["07. latest trading day"].ToString();
            this.PreviousClose = res["Global Quote"]["08. previous close"].ToString();
            this.Change = res["Global Quote"]["09. change"].ToString();
            this.ChangePercent = res["Global Quote"]["10. change percent"].ToString();
        }
    }    
}
