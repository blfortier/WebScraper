using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.ScraperService
{
    public class Stocks
    {
        public string Symbol { get; set; }
        public double LastPrice { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }
        public string Volume { get; set; }
        public string AvgVol { get; set; }
        public string MarketCap { get; set; }

        public Stocks()
        {

        }
        public Stocks(string symbol, double lastPrice,
                    double change, double changePercent, string vol, 
                    string volAvg, string marketCap)
        {
            this.Symbol = symbol;
            this.LastPrice = lastPrice;
            this.Change = change;
            this.ChangePercent = changePercent;
            this.Volume = vol;
            this.AvgVol = volAvg;
            this.MarketCap = marketCap;
        }
    }
}