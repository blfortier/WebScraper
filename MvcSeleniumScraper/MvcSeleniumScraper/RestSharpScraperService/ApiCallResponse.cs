using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.RestSharpScraperService
{
    public class ApiCallResponse
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Change { get; set; }
        public string ChangePercent { get; set; }

        public ApiCallResponse(string symb, string name, string price,
                string change, string chngPrct)
        {
            this.Symbol = symb;
            this.Name = name;
            this.Price = price;
            this.Change = change;
            this.ChangePercent = chngPrct;
        }

        public void DisplayStockInfo()
        {
            Console.WriteLine("Name: {0}", this.Name.Length);
            Console.WriteLine("Symbol: {0}", this.Symbol.Length);
            Console.WriteLine("Price: {0}", this.Price.Length);
            Console.WriteLine("Change: {0}", this.Change.Length);
            Console.WriteLine("Change Percent: {0}", this.ChangePercent.Length);
            Console.WriteLine();
        }

    }
}