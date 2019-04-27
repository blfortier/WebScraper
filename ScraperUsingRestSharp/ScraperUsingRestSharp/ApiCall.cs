using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class ApiCall
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string ChangePercent { get; set; }
        public string MarketCap { get; set; }

        public ApiCall(string symb, string name, string price,
                string chngPrct, string marketCap)
        {
            this.Symbol = symb;
            this.Name = name;
            this.Price = price;
            this.ChangePercent = chngPrct;
            this.MarketCap = marketCap;
        }

        public void DisplayStockInfo()
        {
            Console.WriteLine("Name: {0}", this.Name);
            Console.WriteLine("Symbol: {0}", this.Symbol);
            Console.WriteLine("Price: {0}", this.Price);
            Console.WriteLine("Change Percent: {0}", this.ChangePercent);
            Console.WriteLine("Market Cap: {0}", this.MarketCap);
            Console.WriteLine();
        }

       
    }
}


