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
        private string _changeDetails;
        private string _volume;

        public string Symbol { get => _symbol; set => _symbol = value; }
        public string Name { get => _name; set => _name = value; }
        public string Price { get => _price; set => _price = value; }
        public string ChangeDetails { get => _changeDetails; set => _changeDetails = value; }
        public string Volume { get => _volume; set => _volume = value; }
    }
}
