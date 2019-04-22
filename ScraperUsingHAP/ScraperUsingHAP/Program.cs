using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScraperUsingHAP
{
    class Program
    { 
        static void Main(string[] args)
        {
            List<Stock> stockInfo = new List<Stock>();

            string edwardJonesUrl = "https://www.edwardjones.com/your-watch-list/";

            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(edwardJonesUrl);

            var tableNode = doc.DocumentNode.SelectNodes("//table[contains(@id, 'watchListDetailTableBody')]//tr");
            //-->  //tbody[contains(@id, 'watchListDetailTableBody')]//tr

            if (tableNode != null)
            {
                Console.WriteLine("table found");
                Console.WriteLine("tableNode count: {0}", tableNode.Count);
                Console.WriteLine(tableNode.Descendants().Count());

               int rowCount = 1;
                foreach (var row in tableNode)
                {
                    Console.WriteLine("row name: {0}", row.Name);                    
                    
                    var nameAndSymbolCell = row.SelectSingleNode("th/a");
                    var otherDataCells = row.SelectNodes("td");
                    
                    if (nameAndSymbolCell == null)
                        Console.WriteLine("nameAndSymbolCell null");                    
                        
                    if (otherDataCells == null)
                        Console.WriteLine("otherDataCells null");

                    if (otherDataCells != null && nameAndSymbolCell != null)
                    {
                        string name = nameAndSymbolCell.InnerText;
                        string symbol = nameAndSymbolCell.GetAttributeValue("symbol", null);

                        string lastPrice = otherDataCells[2].InnerText;

                        string change = otherDataCells[3].InnerText;

                        Console.WriteLine("name {0}", name);
                        Console.WriteLine("symbol {0}", symbol);
                        Console.WriteLine("price: {0}", lastPrice);
                        Console.WriteLine("change: {0}", change);

                        Stock stock = new Stock(name, symbol, lastPrice, change);
                        stockInfo.Add(stock);

                        rowCount++;
                        foreach (var stockItem in stockInfo)
                        {
                            Console.WriteLine(stockItem.Name);
                        }
                    }  
                    else
                        Console.WriteLine("Into foreach loop. name and otherData cells null");
                }
            }
            else
                Console.WriteLine("Table not found");
        }

    }
}
