using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> stockSymbol = new List<string>() { "HAS", "TWTR", "USMV",
                "MINI", "KSS", "MRCY", "SVRA", "ANGI", "TDY", "WSO" };

            string joined = string.Join(",", stockSymbol);
            const string api_key = "Jq0GZBASNat6TMVl2pZ5gzTSi2pSKLR8fYZYZK2kZblOdp7W3BBhsMDCinFQ";


            string apiUrl = "https://www.worldtradingdata.com/api/v1/stock?symbol={0}&api_token={1}";
            apiUrl = String.Format(apiUrl, joined, api_key);

            var test = new WorldTradingApi();
            var request = new RestRequest("?symbol={0}&api_token={1}");

            test.Execute<ApiCall>(request);



            Console.WriteLine();








            //   var request = new RestRequest("?symbol={0}&api_token={1}");












            //            for (int i = 0; i < stockSymbol.Count; i++)
            //            {
            //                // string AlphaApi = String.Format("https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={0}&apikey=XAVUBU2B7HKK4KT0", stockSymbol[i]);

            //                var webRequest = System.Net.WebRequest.Create(new Uri(String.Format("https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={0}&apikey=XAVUBU2B7HKK4KT0", stockSymbol[i])));
            //                webRequest.Method = "GET";
            //                webRequest.ContentType = "application/json";

            //                System.Net.WebResponse webRes = webRequest.GetResponseAsync().Result;
            //                System.IO.Stream getStream = webRes.GetResponseStream();
            //                System.IO.StreamReader reader = new System.IO.StreamReader(getStream);

            //                string response = reader.ReadToEnd();

            //                Newtonsoft.Json.Linq.JObject stockData = Newtonsoft.Json.Linq.JObject.Parse(response);

            //                var stockInfo = new ApiResponse(stockData);
            //                var stock = new Stock(stockInfo);
            //                stockList.Add(stock);

            ////                Console.WriteLine(stockData["Global Quote"]["01. symbol"].ToString());
            //  //              Console.WriteLine(stockData["Global Quote"]["05. price"].ToString());
            //            }

            //foreach (var item in stockList)
            //{
            //    item.DisplayStock();
            //    Console.WriteLine();
            //}
        }


        /*{
  "Global Quote": {
    "01. symbol": "HAS",
    "02. open": "100.3000",
    "03. high": "102.8690",
    "04. low": "99.0300",
    "05. price": "100.6500",
    "06. volume": "7061783",
    "07. latest trading day": "2019-04-23",
    "08. previous close": "88.1100",
    "09. change": "12.5400",
    "10. change percent": "14.2322%"
  }
}
{
  "Global Quote": {
    "01. symbol": "TWTR",
    "02. open": "36.9300",
    "03. high": "40.5350",
    "04. low": "36.9100",
    "05. price": "39.7700",
    "06. volume": "103323021",
    "07. latest trading day": "2019-04-23",
    "08. previous close": "34.3900",
    "09. change": "5.3800",
    "10. change percent": "15.6441%"
  }
}
{
  "Global Quote": {
    "01. symbol": "USMV",
    "02. open": "58.9600",
    "03. high": "59.3700",
    "04. low": "58.8300",
    "05. price": "59.3300",
    "06. volume": "3023284",
    "07. latest trading day": "2019-04-23",
    "08. previous close": "58.8300",
    "09. change": "0.5000",
    "10. change percent": "0.8499%"
  }
}
{
  "Global Quote": {
    "01. symbol": "MINI",
    "02. open": "36.8200",
    "03. high": "38.7500",
    "04. low": "35.8400",
    "05. price": "37.0000",
    "06. volume": "713582",
    "07. latest trading day": "2019-04-23",
    "08. previous close": "34.5000",
    "09. change": "2.5000",
    "10. change percent": "7.2464%"
  }
}
*/
    }

    

}
