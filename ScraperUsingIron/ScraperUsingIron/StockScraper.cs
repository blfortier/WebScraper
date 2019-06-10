using IronWebScraper;
using System;
using System.Collections.Generic;
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

            foreach (var row in response.Css("div.watchlist_dynamic1 > table > tbody > tr").Skip(1))
            {
                var stock = new Stock();

                foreach (var name in row.Css("td.rowtitle > a"))
                {
                    //  Console.Write(name.InnerHtml);
                    stock.Name = name.InnerHtml;
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

                Scrape(stock, "Stock.jsonl");
                listOfStocks.Add(stock);

               // Database.InsertStockDataIntoDB(stock);
            }

            foreach (var stock in listOfStocks)
            {
                Database.InsertStockDataIntoDB(stock);
            }
        }
    }
}
