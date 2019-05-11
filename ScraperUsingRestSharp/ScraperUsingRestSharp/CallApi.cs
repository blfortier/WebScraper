using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class CallApi : Database
    {
        private const string _url = "https://www.worldtradingdata.com/api/v1/stock";
        private const string _key = "Jq0GZBASNat6TMVl2pZ5gzTSi2pSKLR8fYZYZK2kZblOdp7W3BBhsMDCinFQ";

        public static void GetStockData(ApiCallData api)
        {
            string joined = string.Join(",", api.Stocks);

            try
            {
                var client = new RestClient(_url);
                var request = new RestRequest("?symbol={symbol}&api_token={api_token}", Method.GET);
                request.AddParameter("symbol", joined);
                request.AddParameter("api_token", _key);
                var response = client.Execute(request);

                var stock = JsonConvert.DeserializeObject<dynamic>(response.Content);

                for (int stockInResponse = 0; stockInResponse < api.Stocks.Count; stockInResponse++)
                {
                    dynamic symbol = stock.data[stockInResponse].symbol.ToString();
                    dynamic name = stock.data[stockInResponse].name.ToString();
                    dynamic price = stock.data[stockInResponse].price.ToString();
                    dynamic change = stock.data[stockInResponse].day_change.ToString();
                    dynamic changePct = stock.data[stockInResponse].change_pct.ToString();

                    var convertToApiCallResponseObject = new ApiCallResponse(symbol, name, price, change, changePct);
                    api.StockList.Add(convertToApiCallResponseObject);

                    InsertStockHistory(convertToApiCallResponseObject);
                    InsertCurrentStock(convertToApiCallResponseObject);
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
