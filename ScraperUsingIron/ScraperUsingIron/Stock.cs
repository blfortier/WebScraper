using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingIron
{
    public class Stock
    {
        private string _symbol;
        private string _name;
        private string _price;
        private string _change;
        private string _changePercent;
        private string _volume;
        private string _marketCap;

        public string Symbol { get => _symbol; set => _symbol = value; }
        public string Name { get => _name; set => _name = value; }
        public string Price { get => _price; set => _price = value; }
        public string Change { get => _change; set => _change = value; }
        public string ChangePercent { get => _changePercent; set => _changePercent = value; }
        public string Volume { get => _volume; set => _volume = value; }
        public string MarketCap { get => _marketCap; set => _marketCap = value; }
    }
}
