//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcSeleniumScraper.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stock
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
    }
}
