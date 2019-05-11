using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.RestSharpScraperService
{
    public class ApiCallResponse
    {
        private string _symbol;
        private string _name;
        private string _price;
        private string _change;
        private string _changePercent;

        public string Symbol { get => _symbol; set => _symbol = value; }
        public string Name { get => _name; set => _name = value; }
        public string Price { get => _price; set => _price = value; }
        public string Change { get => _change; set => _change = value; }
        public string ChangePercent { get => _changePercent; set => _changePercent = value; }

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
            Console.WriteLine("Name: {0}", this.Name);
            Console.WriteLine("Symbol: {0}", this.Symbol);
            Console.WriteLine("Price: {0}", this.Price);
            Console.WriteLine("Change: {0}", this.Change);
            Console.WriteLine("Change Percent: {0}", this.ChangePercent);
            Console.WriteLine();
        }

    }
}