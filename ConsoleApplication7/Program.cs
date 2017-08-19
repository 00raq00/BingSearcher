using BingSearcher;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentDictionary<string, Dictionary<string, string>> cd = new ConcurrentDictionary<string, Dictionary<string, string>> ();

            //Task.WaitAll(GetValue());
            var bng = new BingSearchParameters()
            {
                UseDefaultNetworkCredentials = true,
                SearchAsOnePhrase = true,
                SearchOverPage = "allegro.pl"
            };
            BingSearchParameters.LogActionString @string = Console.WriteLine;
            
            foreach (var url in BingSearchClient.GetBingSearchEntityLazyList("usb3.0 dysk ", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "allegro.pl" }))
            {
                Console.WriteLine(url);
            }


            foreach (var url in BingSearchClient.GetUrlsLazyList("usb3.0 dysk ", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "allegro.pl" }))
            {
                Console.WriteLine(url);
            }

            Task.Run(() => GetValue());

            Console.Read();
        }

        private static async Task GetValue()
        {
            ConcurrentDictionary<string, Dictionary<string, string>> cd;
            var gClient = new GClient();
            
            DateTime t1 = DateTime.Now;
            cd = new ConcurrentDictionary<string, Dictionary<string, string>>();
            foreach (string s in BingSearchClient.GetUrlsLazyList("uwodorniony", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" }))
            {
                //Console.WriteLine("GetData foreach " + s);
                await gClient.parsTescoPageAsync(cd, s);
            }
            DateTime t2 = DateTime.Now;



            Console.WriteLine("lazy: " + (t2 - t1).TotalSeconds);



             t1 = DateTime.Now;
            cd = new ConcurrentDictionary<string, Dictionary<string, string>>();

            gClient.parsTescoPageGetData(BingSearchClient.GetUrlsLazyList("uwodorniony", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" }), cd);
            
             t2 = DateTime.Now;
            

            Console.WriteLine("nie lazy: " + (t2 - t1).TotalSeconds);

            //Console.WriteLine(cd.Keys.Count);
            var tmp = cd.OrderBy(x => x.Key).ToList();
            gClient.CreateCsv(tmp);
        }
    }
}
