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

            string googleFinance = "https://www.google.com/finance#wptab=s:H4sIAAAAAAAAAOPQeMSozC3w8sc9YSmpSWtOXmMU4RJyy8xLzEtO9UnMS8nMSw9ITE_l2cXEHekfGhQfHOLv7B28iJU9DaIGAAUYQO1AAAAA";

            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(googleFinance);


            var root = doc.DocumentNode.SelectNodes("//*[@id='knowledge-finance-wholepage__financial-entities-list']/div");
            //SelectNodes("//div[contains(@class,'ML43JB')]").ToList();

            //foreach (var item in nameNode.Where(n => n.HasClass("TLh8uf")))
            //{
            //    Console.WriteLine(item);
            //}
            //[@class='TLh8uf']");

            Console.WriteLine(root);


            if (root != null)
            {
                Console.WriteLine("yeah");
                Console.WriteLine(root);
                foreach (var item in root.Descendants())
                {
                    // Console.WriteLine(item.InnerText);
                    // Console.WriteLine("in foreach loop");
                    //var name = doc.DocumentNode.SelectSingleNode("/div[1]/g-link/a/div/span[1]").InnerText;
                    Console.WriteLine(item);
                }
            }
            else
                Console.WriteLine("No");


            // //*[@id='knowledge-finance-wholepage__financial-entities-list']/div

            // "//*[contains(@class,'z4Fov')]")
            // //*[@id="knowledge-finance-wholepage__financial-entities-list"]/div[1]/g-link/a/div
            // //*[@id="knowledge-finance-wholepage__financial-entities-list"]
            // #knowledge-finance-wholepage__financial-entities-list
            // <span class="z4Fov">S&amp;P 500 Index</span>
            // //*[@id="knowledge-finance-wholepage__financial-entities-list"]/div[1]/g-link/a/div/span[1]
            // //*[@id="knowledge-finance-wholepage__financial-entities-list"]
            // //*[@id="knowledge-finance-wholepage__financial-entities-list"]/div[1]/g-link/a/div/span[1]
            // //*[@id="knowledge-finance-wholepage__financial-entities-list"]/div[1]/g-link/a/div/span[1]
            // //*[@id="knowledge-finance-wholepage__financial-entities-list"]

        }

    }
}
