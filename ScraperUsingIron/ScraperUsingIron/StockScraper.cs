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

            HttpIdentity id = new HttpIdentity();
            id.NetworkUsername = "webdriverproj";
            id.NetworkPassword = "wdScraper135";
            Identities.Add(id);
            this.Request("https://www.msn.com/en-us/money/watchlist", Parse, id);
        }

        public override void Parse(Response response)
        {
            // //*[@id="watchlistsummarytab"]/tr
            // #movie-featured > div
            foreach (var Trs in response.Css("#watchlistsummarytab  > tr"))
            {
                var stock = new Stock();
                //stock.Symbol = 
            }
        }
    }
}
