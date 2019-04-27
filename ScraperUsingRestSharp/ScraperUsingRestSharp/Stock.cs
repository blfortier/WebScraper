using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string LastPrice { get; set; }
        public string ChangePercent { get; set; }

        public Stock()
        {
        }

        public Stock(ApiCall res)
        {
            this.Symbol = res.Symbol;
            this.Name = res.Name;
            this.LastPrice = res.Price;
            this.ChangePercent = res.ChangePercent;
        }

        public void DisplayStock()
        {
            Console.WriteLine("Symbol: {0}", this.Symbol);
            Console.WriteLine("Name: {0}", this.Name);
            Console.WriteLine("Last Price: {0}", this.LastPrice);
            Console.WriteLine("Change %: {0}", this.ChangePercent);

        }
    }
}
