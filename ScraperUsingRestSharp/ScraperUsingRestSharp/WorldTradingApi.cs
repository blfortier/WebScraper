using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class WorldTradingApi
    {
        const string BaseUrl = "https://www.worldtradingdata.com/api/v1/stock";
        const string ApiKey = "Jq0GZBASNat6TMVl2pZ5gzTSi2pSKLR8fYZYZK2kZblOdp7W3BBhsMDCinFQ";

        readonly IRestClient _client;

        public WorldTradingApi()
        {
            _client = new RestClient(BaseUrl);
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            request.AddParameter("api_token", ApiKey, ParameterType.UrlSegment);
            var response = _client.Execute<T>(request);
            Console.WriteLine("In Execute");

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response";
                var apiException = new ApplicationException(message, response.ErrorException);
                throw apiException;
            }
            return response.Data;
        }

        //public ApiCall GetCall(string stocksToCall)
        //{
        //    var request = new RestRequest("?symbol={symbol}&api_token={api_token}");

        //    request.AddParameter("symbol", stocksToCall, ParameterType.UrlSegment);



        //}

    }
}
