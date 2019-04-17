﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.ScraperService
{
    public class ScrapedData
    {
        public IList<IWebElement> StockSymbols { get; set; }
        public IList<IWebElement> StockLastPrices { get; set; }
        public IList<IWebElement> StockChanges { get; set; }
        public IList<IWebElement> StockChangePercents { get; set; }
        public IList<IWebElement> StockMarketTimes { get; set; }
        public IList<IWebElement> StockVolumes { get; set; }
        public IList<IWebElement> StockAvgVolumes { get; set; }
        public IList<IWebElement> StockShares { get; set; }
        public IList<IWebElement> StockMarketCaps { get; set; }

        public ScrapedData(IList<IWebElement> symbols, IList<IWebElement> lastPrices,
                    IList<IWebElement> changes, IList<IWebElement> changePercents,
                    IList<IWebElement> marketTimes, IList<IWebElement> volumes,
                    IList<IWebElement> avgVolumes, IList<IWebElement> shares, IList<IWebElement> marketCaps)
        {
            this.StockSymbols = symbols;
            this.StockLastPrices = lastPrices;
            this.StockChanges = changes;
            this.StockChangePercents = changePercents;
            this.StockMarketTimes = marketTimes;
            this.StockVolumes = volumes;
            this.StockAvgVolumes = avgVolumes;
            this.StockShares = shares;
            this.StockMarketCaps = marketCaps;
        }
    }
}