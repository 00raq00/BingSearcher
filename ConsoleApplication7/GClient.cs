using HtmlAgilityPack;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApplication7
{
    public class BingSearchClient1
    {

        internal List<string> GetData(ConcurrentDictionary<string, Dictionary<string, string>> cd, string searchedString = "utwardzony")
        {
            List<string> ls = new List<string>();
            HtmlNodeCollection tmo = null;
            string searchUrl = "/search?q=site%3Aezakupy.tesco.pl+" + searchedString + "&first=1&FORM=PORE";
            do
            {

                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load("http://www.bing.com" + searchUrl);

                foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//li/h2/a"))
                {
                    // Console.WriteLine(" URL: " + row.Attributes["href"].Value);

                    ls.Add(row.Attributes["href"].Value);
                    //parsTescoPage(row.Attributes["href"].Value);
                   // Console.WriteLine(row.InnerText);
                }

                tmo = doc.DocumentNode.SelectNodes("//a[@class='sb_pagN']");
                if (tmo != null)
                {
                    var row1 = tmo.Last();
                    {
                        //Console.WriteLine(" URL!: " + row1.Attributes["href"].Value);
                        searchUrl = row1.Attributes["href"].Value.Replace("&amp;", "&").Replace("%3a", "%3A");
                        //Console.WriteLine(row1.InnerText);
                    }
                }
            }
            while (tmo != null);

            return ls;
        }

        internal IEnumerable<string> GetDataLazy(ConcurrentDictionary<string, Dictionary<string, string>> cd, string searchedString = "utwardzony")
        {
            List<string> ls = new List<string>();
            HtmlNodeCollection tmo = null;
            string searchUrl = "/search?q=site%3Aezakupy.tesco.pl+" + searchedString + "&first=1&FORM=PORE";
            do
            {

                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load("http://www.bing.com" + searchUrl);

                foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//li/h2/a"))
                {
                    // Console.WriteLine(" URL: " + row.Attributes["href"].Value);

                  yield return  row.Attributes["href"].Value;
                    //parsTescoPage(row.Attributes["href"].Value);
                    //Console.WriteLine(row.InnerText);
                }

                tmo = doc.DocumentNode.SelectNodes("//a[@class='sb_pagN']");
                if (tmo != null)
                {
                    var row1 = tmo.Last();
                    {
                        //Console.WriteLine(" URL!: " + row1.Attributes["href"].Value);
                        searchUrl = row1.Attributes["href"].Value.Replace("&amp;", "&").Replace("%3a", "%3A");
                        //Console.WriteLine(row1.InnerText);
                    }
                }
            }
            while (tmo != null);
            yield break;

        }

        internal void parsTescoPageGetData(IEnumerable<string> enumerable, ConcurrentDictionary<string, Dictionary<string, string>> cd)
        {
            Parallel.ForEach(enumerable, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, l =>
            {
                parsTescoPage(cd, l);
            });
        }

        internal void parsTescoPageGetData(List<string> ls, ConcurrentDictionary<string, Dictionary<string, string>> cd, string searchedString = "utwardzony")
        {
            
            Parallel.ForEach(ls, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, l =>
            {
                parsTescoPage(cd, l);
            });
        }

        internal void CreateCsv(List<KeyValuePair<string, Dictionary<string, string>>> tmp)
        {
            
        }

        internal void CreateCsv(ConcurrentDictionary<string, Dictionary<string, string>> cd)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var s in cd)
            {
                sb.AppendLine($"{s.Key};{s.Value["ingredients"]};{s.Value["img"]};");
            }
        }

        internal void parsTescoPage(ConcurrentDictionary<string, Dictionary<string, string>> cd, string value, bool triedAgain = false)
        {
            try
            {
                string name = "";
                string ingredients = "";
                string img = "";

                HtmlWeb web = new HtmlWeb();
                web.UsingCache = false;
                HtmlDocument doc = web.Load(value);

                foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//h1[@class='product-title']"))
                {
                    //Console.WriteLine("TESCO!: " + row.InnerText);
                    name = row.InnerText;
                }

                foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//span[@class='product-image-wrapper']/img"))
                {

                    //Console.WriteLine("TESCO!: " + row.Attributes["src"].Value);
                    img = row.Attributes["src"].Value;
                }

                foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//div[@class='brand-bank']//div[@class='groupItem']//h3"))
                {
                    if (row.InnerText.Equals("Składniki"))
                    {
                        //Console.WriteLine(row.ParentNode.SelectNodes("div/p").First().InnerText + "\n\n");
                        ingredients = row.ParentNode.SelectNodes("div/p").First().InnerText + "\n\n";
                        break;
                    }

                }
                Dictionary<string, string> dic = new Dictionary<string, string>() { { "ingredients", ingredients }, { "img", img } };
                cd[name] = dic;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (!triedAgain) parsTescoPage(cd, value, true);
            }
        }
        internal async Task parsTescoPageAsync(ConcurrentDictionary<string, Dictionary<string, string>> cd, string value, bool triedAgain = false)
        {

            try
            {
             await   Task.Run(() =>
                {
                    string name = "";
                    string ingredients = "";
                    string img = "";

                    HtmlWeb web = new HtmlWeb();
                    web.UsingCache = false;
                    HtmlDocument doc = web.Load(value);

                    foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//h1[@class='product-title']"))
                    {
                        Console.WriteLine("TESCO!: " + row.InnerText);
                        name = row.InnerText;
                    }

                    foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//span[@class='product-image-wrapper']/img"))
                    {

                        Console.WriteLine("TESCO!: " + row.Attributes["src"].Value);
                        img = row.Attributes["src"].Value;
                    }

                    foreach (
                        HtmlNode row in
                        doc.DocumentNode.SelectNodes("//div[@class='brand-bank']//div[@class='groupItem']//h3"))
                    {
                        if (row.InnerText.Equals("Składniki"))
                        {
                            Console.WriteLine(row.ParentNode.SelectNodes("div/p").First().InnerText + "\n\n");
                            ingredients = row.ParentNode.SelectNodes("div/p").First().InnerText + "\n\n";
                            break;
                        }

                    }
                    Dictionary<string, string> dic = new Dictionary<string, string>()
                    {
                        {"ingredients", ingredients},
                        {"img", img}
                    };
                    cd[name] = dic;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (!triedAgain) await parsTescoPageAsync(cd, value, true);
            }
        }
    }
}