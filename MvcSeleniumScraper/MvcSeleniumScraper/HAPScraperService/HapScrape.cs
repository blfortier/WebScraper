using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MvcSeleniumScraper.HAPScraperService
{
    public class HapScrape : Database
    {
        private string _url;
        private HtmlWeb _web;
        private HtmlDocument _doc;

        public string Url { get => _url; set => _url = value; }
        public HtmlWeb Web { get => _web; set => _web = value; }
        public HtmlDocument Doc { get => _doc; set => _doc = value; }

        public HapScrape(string url)
        {
            this.Url = url;

            Web = new HtmlWeb();
            Doc = Web.Load(Url);
        }

        public void ScrapeStockData()
        {
            Stock stock;
            List<Stock> stockInfo = new List<Stock>();

            var tableNode = Doc.DocumentNode.SelectNodes("//*[@id='_active']/table/tr");

            if (tableNode != null)
            {
                foreach (var item in tableNode)
                {
                    string symbol = item.SelectSingleNode("td/h3/a").InnerText;
                    string name = item.SelectSingleNode("td[2]/b/a").InnerText;
                    string lastPrice = item.SelectSingleNode("td[4]").InnerText.Replace(" ", string.Empty);
                    HtmlNode changeNode = item.SelectSingleNode("td[5]/span");

                    string changeString = "";
                    if (changeNode == null)
                        changeString = "N/A";
                    else
                        changeString = ParseChangePercent(changeNode);

                    stock = new Stock(name, symbol, lastPrice, changeString);
                    stockInfo.Add(stock);
                    InsertStockDataIntoDB(stock);
                }
            }

        }

        public static string ParseChangePercent(HtmlNode innerText)
        {
            string change = innerText.InnerText;

            Match match = Regex.Match(change, @"([^;]+$)");
            string modifiedString = match.Groups[1].Value;

            if (innerText.Attributes["class"].Value == "green")
                modifiedString = "+" + modifiedString + "%";
            else
                modifiedString = "-" + modifiedString + "%";

            return modifiedString;
        }
    }
}