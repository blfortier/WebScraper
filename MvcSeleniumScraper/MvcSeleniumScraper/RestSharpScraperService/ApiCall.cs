//using Newtonsoft.Json;
////using RestSharp;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace MvcSeleniumScraper.RestSharpScraperService
//{
//    public class ApiCall
//    {
//        private string _apiKey = "Jq0GZBASNat6TMVl2pZ5gzTSi2pSKLR8fYZYZK2kZblOdp7W3BBhsMDCinFQ";
//        private string _baseUrl = "https://www.worldtradingdata.com/api/v1/stock";
//        public List<ApiCallResponse> stockList;
//        RestClient client;
//        RestRequest req;

//        public ApiCall(List<string> stocks)
//        {
//            this.stockList = new List<ApiCallResponse>();
//            client = new RestClient(_baseUrl);
//            req = new RestRequest("?symbol={symbol}&api_token={api_token}", Method.GET);
//        }

//        public void Scrape()
//        {
//            Console.WriteLine("scrape");
//            string joined = string.Join(",", stockList);

//            try
//            {
//                req.AddParameter("symbol", joined);
//                req.AddParameter("api_token", _apiKey);
//                var response = client.Execute(req);

//                var stock = JsonConvert.DeserializeObject<dynamic>(response.Content);

//                for (int stockInResponse = 0; stockInResponse < stockList.Count; stockInResponse++)
//                {
//                    var symbol = stock.data[stockInResponse].symbol.ToString();
//                    var name = stock.data[stockInResponse].name.ToString();
//                    var price = stock.data[stockInResponse].price.ToString();
//                    var change = stock.data[stockInResponse].day_change.ToString();
//                    var changePct = stock.data[stockInResponse].change_pct.ToString();

//                    Console.WriteLine("stuff: {0} {1} {2} {3} {4}", symbol, name, price, change, changePct);

//                    var convertToApiCallResponseObject = new ApiCallResponse(symbol, name, price, change, changePct);
//                    stockList.Add(convertToApiCallResponseObject);
//                    Database.InsertStockDataIntoDatabase(convertToApiCallResponseObject);
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("Error making GET call...");
//                throw e;
//            }

//        }
//    }
//}