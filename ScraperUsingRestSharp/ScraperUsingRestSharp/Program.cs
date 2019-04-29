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
            List<string> stockSymbols = new List<string>() { "HAS", "TWTR", "USMV",
                   "MINI", "KSS"};

            ApiCall callAPi = new ApiCall(stockSymbols);
            callAPi.Scrape();

            //List<ApiCallResponse> stockList = new List<ApiCallResponse>();

            //string joined = string.Join(",", stockSymbol);
            //const string api_key = "Jq0GZBASNat6TMVl2pZ5gzTSi2pSKLR8fYZYZK2kZblOdp7W3BBhsMDCinFQ";

            //string apiUrl = "https://www.worldtradingdata.com/api/v1/stock";

            //try
            //{
            //    var client = new RestClient(apiUrl);
            //    var request = new RestRequest("?symbol={symbol}&api_token={api_token}", Method.GET);
            //    request.AddParameter("symbol", joined);
            //    request.AddParameter("api_token", api_key);
            //    var response = client.Execute(request);

            //    var stock = JsonConvert.DeserializeObject<dynamic>(response.Content);

            //    for (int stockInResponse = 0; stockInResponse < stockSymbol.Count; stockInResponse++)
            //    {
            //        var symbol = stock.data[stockInResponse].symbol.ToString();
            //        var name = stock.data[stockInResponse].name.ToString();
            //        var price = stock.data[stockInResponse].price.ToString();
            //        var change = stock.data[stockInResponse].day_change.ToString();
            //        var changePct = stock.data[stockInResponse].change_pct.ToString();

            //        //     Console.WriteLine("stuff: {0} {1} {2} {3} {4}", symbol, name, price, change, changePct);

            //        var convertToApiCallResponseObject = new ApiCallResponse(symbol, name, price, change, changePct);
            //        stockList.Add(convertToApiCallResponseObject);
            //        Database.InsertStockDataIntoDatabase(convertToApiCallResponseObject);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error making GET call...");
            //    throw e;
            //}
        }
    }
}


   