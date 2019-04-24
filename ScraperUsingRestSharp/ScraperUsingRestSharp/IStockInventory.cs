using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    interface IStockInventory
    {
        IEnumerable<Stock> GetAll();
        Stock Get(string symbol);

       
    }
   
}

/*
 
"symbol": "AAPL",
            "name": "Apple Inc.",
            "price": "208.03",
            "currency": "USD",
            "price_open": "207.35",
            "day_high": "208.40",
            "day_low": "207.23",
            "52_week_high": "233.47",
            "52_week_low": "142.00",
            "day_change": "0.54",
            "change_pct": "0.26",
            "close_yesterday": "207.48",
            "market_cap": "980636787355",
            "volume": "159952",
            "volume_avg": "28631657",
            "shares": "4715280000",
            "stock_exchange_long": "NASDAQ Stock Exchange",
            "stock_exchange_short": "NASDAQ",
            "timezone": "EDT",
            "timezone_name": "America/New_York",
            "gmt_offset": "-14400",
            "last_trade_time": "2019-04-24 09:33:30" 
*/

