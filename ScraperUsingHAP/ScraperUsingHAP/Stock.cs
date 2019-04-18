using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingHAP
{
    class Stock
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
    }   
}
