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
        public double LastPrice { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }
        public string MarketTime { get; set; }
        public double Volume { get; set; }
        public double AvgVol { get; set; }
        public int Shares { get; set; }
        public double MarketCap { get; set; }

        public Stock()
        {

        }
        public Stock(int id, string symbol, double lastPrice,
                    double change, double changePercent, string time, 
                    double vol, double volAvg, int shares, double marketCap)
        {
            this.Id = id;
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
