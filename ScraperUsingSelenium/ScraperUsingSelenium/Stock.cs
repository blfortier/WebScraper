using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScraperUsingSelenium
{
    class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public float LastPrice { get; set; }
        public float Change { get; set; }
        public float ChangePercent { get; set; }
        public string Currency { get; set; }
        public TimeSpan MarketTime { get; set; }
        public int Volume { get; set; }
        public float AvgVol { get; set; }
        public decimal MarketCap { get; set; }
    }
}
