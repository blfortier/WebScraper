using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.HAPScraperService
{
    public class Stock
    {
        private string _name;
        private string _symbol;
        private string _lastPrice;
        private string _change;

        public string Name { get => _name; set => _name = value; }
        public string Symbol { get => _symbol; set => _symbol = value; }
        public string LastPrice { get => _lastPrice; set => _lastPrice = value; }
        public string Change { get => _change; set => _change = value; }

        public Stock()
        {
        }

        public Stock(string name, string symbol, string lastPrice, string change)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.LastPrice = lastPrice;
            this.Change = change;
        }

        public void DisplayStock()
        {
            Console.WriteLine("Name: {0}", this.Name);
            Console.WriteLine("Symbol: {0}", this.Symbol);
            Console.WriteLine("Last Price: {0}", this.LastPrice);
            Console.WriteLine("Change %: {0}", this.Change);
        }
    }
}