using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class ApiCallResponse
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
            Console.WriteLine("Name: {0}", this.Name);
            Console.WriteLine("Symbol: {0}", this.Symbol);
            Console.WriteLine("Price: {0}", this.Price);
            Console.WriteLine("Change: {0}", this.Change);
            Console.WriteLine("Change Percent: {0}", this.ChangePercent);
            Console.WriteLine();
        }

       
    }
}


