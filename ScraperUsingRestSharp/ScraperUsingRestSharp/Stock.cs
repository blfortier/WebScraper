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
        public string LastPrice { get; set; }
        public string Change { get; set; }
        public string ChangePercent { get; set; }

        public Stock()
        {
        }

        public Stock(ApiResponse res)
        {
            this.Symbol = res.Symbol;
            this.LastPrice = res.Price;
            this.Change = res.Change;
            this.ChangePercent = res.ChangePercent;
        }

        public void DisplayStock()
        {
            Console.WriteLine("Symbol: {0}", this.Symbol);
            Console.WriteLine("Last Price: {0}", this.LastPrice);
            Console.WriteLine("Change %: {0}", this.Change);
            Console.WriteLine("Change %: {0}", this.ChangePercent);

        }
    }
}
