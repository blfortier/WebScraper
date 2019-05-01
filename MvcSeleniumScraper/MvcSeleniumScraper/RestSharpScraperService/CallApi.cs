using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.RestSharpScraperService
{
    public class CallApi
    {
        public static void GetStockData(ApiCall api)
        {
            string joined = string.Join(",", api.Stocks);

            try
            {
                var client = new RestClient(api.Url);
                var request = new RestRequest("?symbol={symbol}&api_token={api_token}", Method.GET);
                request.AddParameter("symbol", joined);
                request.AddParameter("api_token", api.Key);
                var response = client.Execute(request);

                var stock = JsonConvert.DeserializeObject<dynamic>(response.Content);

                for (int stockInResponse = 0; stockInResponse < api.Stocks.Count; stockInResponse++)
                {
                    var symbol = stock.data[stockInResponse].symbol.ToString();
                    var name = stock.data[stockInResponse].name.ToString();
                    var price = stock.data[stockInResponse].price.ToString();
                    var change = stock.data[stockInResponse].day_change.ToString();
                    var changePct = stock.data[stockInResponse].change_pct.ToString();

                    //     Console.WriteLine("stuff: {0} {1} {2} {3} {4}", symbol, name, price, change, changePct);

                    var convertToApiCallResponseObject = new ApiCallResponse(symbol, name, price, change, changePct);
                    api.StockList.Add(convertToApiCallResponseObject);
                    Database.InsertStockDataIntoDatabase(convertToApiCallResponseObject);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error making GET call...");
                throw e;
            }
        }
    }
}