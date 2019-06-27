using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.IronScraperService
{
    public class ISStock
    {
        private string _name;
        private string _symbol;
        private string _price;
        private string _priceChange;
        private string _changePercent;
        private string _volume;

        public string Name { get => _name; set => _name = value; }
        public string Symbol { get => _symbol; set => _symbol = value; }
        public string Price { get => _price; set => _price = value; }
        public string PriceChange { get => _priceChange; set => _priceChange = value; }
        public string ChangePercent { get => _changePercent; set => _changePercent = value; }

        public string Volume { get => _volume; set => _volume = value; }

        public ISStock() { }
        public ISStock(string name, string symb, string price, string change, string changePercent, string volume)
        {
            this.Name = name;
            this.Symbol = symb;
            this.Price = price;
            this.PriceChange = change;
            this.ChangePercent = ChangePercent;
            this.Volume = volume;
        }
        public void DisplayStockInfo()
        {
            Console.WriteLine("Name: {0}", this.Name);
            Console.WriteLine("----------------------");
            Console.WriteLine("Symbol: {0}", this.Symbol);
            Console.WriteLine("Price: {0}", this.Price);
            Console.WriteLine("Price change: {0}", this.PriceChange);
            Console.WriteLine("Change percent: {0}", this.ChangePercent);
            Console.WriteLine("Volume: {0}", this.Volume);
        }
    }
}