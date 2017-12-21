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
    public class GClient
    {
        internal void parsTescoPage(ConcurrentDictionary<string, Dictionary<string, string>> cd, string value, bool triedAgain = false)
        {
            try
            {
                {
                    string name = "";
                    string ingredients = "";
                    string img = "";

                  HtmlWeb web = new HtmlWeb();
                 
                    web.UsingCache = false;
                    HtmlDocument doc = web.LoadFromBrowser(value);

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
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (!triedAgain) parsTescoPage(cd, value, true);
            }
        }
    }
}