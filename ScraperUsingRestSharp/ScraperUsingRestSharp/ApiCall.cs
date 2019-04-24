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
        public string Currency { get; set; }
        public string PriceOpen { get; set; }
        public string DayHigh { get; set; }
        public string DayLow { get; set; }
        public string WeekHigh52 { get; set; }
        public string WeekLow52 { get; set; }
        public string DayChange { get; set; }
        public string ChangePercent { get; set; }
        public string CloseYesterday { get; set; }
        public string MarketCap { get; set; }
        public string Volume { get; set; }
        public string VolumeAvg { get; set; }
        public string Shares { get; set; }
        public string StockExLong { get; set; }
        public string StockExShort { get; set; }
        public string Timezone { get; set; }
        public string TzName { get; set; }
        public string GmtOffset { get; set; }
        public string LastTrade { get; set; }

        public ApiCall()
        {

        }

        public void DisplayStockInfo()
        {
            Console.WriteLine("Name: {0}", this.Name);
            Console.WriteLine("Symbol: {0}", this.Symbol);
            Console.WriteLine("Price: {0}", this.Name);
            Console.WriteLine("Change Percent: {0}", this.ChangePercent);
            Console.WriteLine();
        }

       
    }
}


