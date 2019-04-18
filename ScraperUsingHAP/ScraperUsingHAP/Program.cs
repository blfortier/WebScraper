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
            var stockInfo = new List<Stock>();

            const string GoogleFinancePage = "https://www.google.com/finance";

            var web = new HtmlWeb();
            var htmlDoc = web.Load(GoogleFinancePage);

            string stock;

            if (htmlDoc.DocumentNode != null && htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Any())
            {
                
            }
                    // //*[@id="knowledge-finance-wholepage__financial-entities-list"]

        }
    }
}
