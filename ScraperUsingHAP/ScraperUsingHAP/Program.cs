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
                foreach (var item in tableNode)
                {
                   // Console.WriteLine(item.Name);

                    var symbol = item.SelectSingleNode("td/h3/a").InnerText;
                   // Console.WriteLine("symbol: {0}", symbol);

                    var name = item.SelectSingleNode("td[2]/b/a").InnerText;
                 //   Console.WriteLine("name: {0}", name);

                    var lastPrice = item.SelectSingleNode("td[4]").InnerText.Replace(" ", string.Empty);
                 //   Console.WriteLine("last price: {0}", lastPrice);

                    var changeNode = item.SelectSingleNode("td[5]/span");
                    var changeString = ParseChangePercent(changeNode);

                 //   Console.WriteLine("change percent: {0}", changeString);

                    stock = new Stock(name, symbol, lastPrice, changeString);
                    stockInfo.Add(stock);
                    Console.WriteLine("Stock object created for {0}", stock.Name);
                    Database.InsertStockDataIntoDatabase(stock);
                    Console.WriteLine("Stock inserted into database...");

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
