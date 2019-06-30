using IronWebScraper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingIron
{
    public class StockScraper : WebScraper
    {
        List<Stock> listOfStocks = new List<Stock>();

        public override void Init()
        {
            this.LoggingLevel = WebScraper.LogLevel.All;
            this.Request("https://markets.financialcontent.com/stocks/stocks/dashboard/mostactive", Parse);
        }

        public override void Parse(Response response)
        {
            foreach (var row in response.Css("div.watchlist_dynamic1 > table > tbody > tr").Skip(1))
            {
                 var stock = new Stock();

                foreach (var name in row.Css("td.rowtitle > a"))
                {
                    stock.Name = ParseName(name);
                }

                foreach (var symbol in row.Css("td.col_symbol"))
	            {
                    stock.Symbol = symbol.InnerHtml;
	            }

                foreach (var price in row.Css("td.col_price"))
	            {
                    stock.Price = price.InnerHtml;
	            }

                foreach (var change in row.Css("td.col_changecompound"))
	            {
                    List<string> changeInfo = change.InnerText.Split().ToList();

                    stock.PriceChange = changeInfo[0];
                    stock.ChangePercent = changeInfo[1];
	            }
                
                foreach (var dollarVol in row.Css("td.col_dollarvolume"))
	            {
                    stock.Volume = dollarVol.InnerText;
	            }

                Console.WriteLine();

                listOfStocks.Add(stock);
                Scrape(stock, "Stock.jsonl");
            }
        }

        public void AddStockToDatabase()
        {
            foreach (var stock in listOfStocks)
            {
                Database.InsertStockDataIntoDB(stock);
            }
        }

        public static string ParseName(HtmlNode name)
        {
            StringBuilder changedName = new StringBuilder(name.InnerText);

            for (int letter = 0; letter < changedName.Length; letter++)
            {
                if (changedName[letter] == '&')
                    changedName.Replace("&amp;", "&");
            }

            return changedName.ToString();
        }

        public static List<string> ParseChange(HtmlNode changePercent)
        {
            List<string> changes = changePercent.InnerText.Split().ToList();
            string percentChange = changes[1];

            StringBuilder parsedString = new StringBuilder(percentChange);

            List<string> formattedStrings = new List<string>();

            parsedString.Remove(parsedString[0],1);
            for (int i = 0; i < parsedString.Length; i++)
            {
                if (parsedString[i] == '(' || parsedString[i] == ')')
                    parsedString.Remove(parsedString[i], 1);
            }
            formattedStrings.Add(changes[0]);
            formattedStrings.Add(parsedString.ToString());

            return formattedStrings;
        }
    }
}

