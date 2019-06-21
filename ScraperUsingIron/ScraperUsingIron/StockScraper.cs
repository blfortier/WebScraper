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
        public override void Init()
        {
            this.LoggingLevel = WebScraper.LogLevel.All;
            this.Request("https://markets.financialcontent.com/stocks/stocks/dashboard/mostactive", Parse);
        }

        public override void Parse(Response response)
        {
            List<Stock> listOfStocks = new List<Stock>();
            var stock = new Stock();

            foreach (var row in response.Css("div.watchlist_dynamic1 > table > tbody > tr").Skip(1))
            {
                foreach (var name in row.Css("td.rowtitle > a"))
                {
                    //  Console.Write(name.InnerHtml);
                    stock.Name = ParseName(name);
                }

                foreach (var symbol in row.Css("td.col_symbol"))
	            {
                 //   Console.WriteLine(symbol.InnerHtml);
                    stock.Symbol = symbol.InnerHtml;
	            }

                foreach (var price in row.Css("td.col_price"))
	            {
                //    Console.WriteLine(price.InnerHtml);
                    stock.Price = price.InnerHtml;
	            }

                foreach (var change in row.Css("td.col_changecompound"))
	            {
                  //  Console.WriteLine(change.InnerText);
                    stock.ChangeDetails = change.InnerText;
	            }
                
                foreach (var dollarVol in row.Css("td.col_dollarvolume"))
	            {
                  //  Console.WriteLine("vol: {0}", dollarVol.InnerText);
                    stock.Volume = dollarVol.InnerText;
	            }

                listOfStocks.Add(stock);

                Scrape(stock, "Stock.jsonl");

              //  Database.InsertStockDataIntoDB(stock);

            }
        }

        public static string ParseName(HtmlNode name)
        {
            StringBuilder changedName = new StringBuilder(name.InnerText);

            for (int letter = 0; letter < changedName.Length; letter++)
            {
                if (changedName[letter] == '&')
                {
                    changedName.Replace("&amp;", "&");
                }
            }

            return changedName.ToString();
        }
    }
}

// Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True
// Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False