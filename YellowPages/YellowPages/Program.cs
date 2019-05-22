using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowPages
{
    class Program
    {
        static void Main(string[] args)
        {
            Scraper searchYP = new Scraper();
            searchYP.NavigateToYellowPagesSearchBox();
            searchYP.GetBusinessData();
        }
    }
}
