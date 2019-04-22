using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ScraperUsingHAP
{
    class Program
    { 
        static void Main(string[] args)
        {
            List<Stock> stockInfo = new List<Stock>();
            Stock stock;

            string nasdaqWebsite = "https://www.nasdaq.com/markets/most-active.aspx";

            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(nasdaqWebsite);

            var tableNode = doc.DocumentNode.SelectNodes("//*[@id='_active']/table/tr");

            if (tableNode != null)
            {
                Console.WriteLine("table found");
                Console.WriteLine("tableNode count: {0}", tableNode.Count);

                foreach (var item in tableNode)
                {
                   // Console.WriteLine(item.Name);

                    var symbol = item.SelectSingleNode("td/h3/a").InnerText;
                    Console.WriteLine("symbol: {0}", symbol);

                    var name = item.SelectSingleNode("td[2]/b/a").InnerText;
                    Console.WriteLine("name: {0}", name);

                    var lastPrice = item.SelectSingleNode("td[4]").InnerText;
                    Console.WriteLine("last price: {0}", lastPrice);

                    var changeNode = item.SelectSingleNode("td[5]/span");
                    string change = changeNode.InnerText;
                    Console.WriteLine("change reg: {0}", change);
                    Match match = Regex.Match(change, @"([^;]+$)");
                    string changeString = match.Groups[1].Value;

                    if (changeNode.Attributes["class"].Value == "green")
                        changeString = "+" + changeString;
                    else
                        changeString = "-" + changeString;

                    Console.WriteLine("change percent: {0}", changeString);

                    stock = new Stock(name, symbol, lastPrice, changeString);
                    stockInfo.Add(stock);
                    Console.WriteLine("Stock object created for {0}", stock.Name);

                }

            }
        }

    }
}
