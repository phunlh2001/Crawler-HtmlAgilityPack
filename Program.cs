using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestCrawlData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = true,
                OverrideEncoding = Encoding.UTF8
            };

            HtmlDocument doc = htmlWeb.Load("https://www.imdb.com/search/title/?groups=top_100&sort=user_rating,desc&count=100");

            var threadItems = doc.DocumentNode.QuerySelectorAll("h3.lister-item-header").ToList();
            var items = new List<object>();

            foreach (var item in threadItems)
            {
                var link = item.QuerySelector("a").Attributes["href"].Value;
                items.Add(new { link });
            }

            string path = Environment.CurrentDirectory + @"\data.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (var item in items)
                    {
                        sw.WriteLine(item);
                    }
                }
                Console.WriteLine("Crawl successful");
            }

            // using (StreamReader sr = new StreamReader(path))
            // {
            //     string s;
            //     while ((s = sr.ReadLine()) != null)
            //     {
            //         Console.WriteLine(s);
            //     }
            // }
        }
    }
}
