using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScraperUsingSelenium
{
    class Stock
    {
        public string Symbol { get; set; }
        public double LastPrice { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }
        public string MarketTime { get; set; }
        public string Volume { get; set; }
        public string AvgVol { get; set; }
        public string Shares { get; set; }
        public string MarketCap { get; set; }

        public Stock()
        {

        }
        public Stock(string symbol, double lastPrice,
                    double change, double changePercent, string time, 
                    string vol, string volAvg, string shares, string marketCap)
        {
            this.Symbol = symbol;
            this.LastPrice = lastPrice;
            this.Change = change;
            this.ChangePercent = changePercent;
            this.MarketTime = time;
            this.Volume = vol;
            this.AvgVol = volAvg;
            this.Shares = Shares;
            this.MarketCap = marketCap;
        }

    }
}
