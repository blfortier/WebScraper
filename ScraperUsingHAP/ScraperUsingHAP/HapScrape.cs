using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScraperUsingHAP
{
    class HapScrape : Database
    {
        HtmlWeb web;
        HtmlDocument doc;
        private string _url;

        public string Url { get => _url; set => _url = value; }

        public HapScrape(string url)
        {
            this.Url = url;

            web = new HtmlWeb();
            doc = web.Load(url);
        }

        public void ScrapeStockData()
        {
            Stock stock;
            List<Stock> stockInfo = new List<Stock>();

            HtmlNodeCollection tableNode = doc.DocumentNode.SelectNodes("//*[@id='_active']/table/tr");

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
                    InsertStocksIntoDB(stock);
                }
            }
        }

        public static string ParseChangePercent(HtmlNode innerText)
        {
            string change = innerText.InnerText;

            Match match = Regex.Match(change, @"([^;]+$)");
            string modifiedString = match.Groups[1].Value;

            if (innerText.Attributes["class"].Value == "green")
                modifiedString = "+" + modifiedString;
            else
                modifiedString = "-" + modifiedString;

            return modifiedString;
        }
    }
}