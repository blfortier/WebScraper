using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.HAPScraperService
{
    public class Stock
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string LastPrice { get; set; }
        public string Change { get; set; }

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