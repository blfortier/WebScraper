using System;

namespace WebScraper
{
    partial class Program
    {
        public class Stocks
        {
            public int Id { get; set; }
            public string Symbol { get; set; }
            public float LastPrice { get; set; }
            public float Change { get; set; }
            public float ChangePercent { get; set; }
            public string Currency { get; set; }
            public DateTime MarketTime { get; set; }
            public int Volume { get; set; }
            public float AvgVol { get; set; }
            public int MarketCap { get; set; }
        }
    }
}
