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
            var GClient = new BingSearchClient1();
            
            DateTime t1 = DateTime.Now;
            cd = new ConcurrentDictionary<string, Dictionary<string, string>>();
            foreach (string s in GClient.GetData(cd, "uwodorniony"))
            {
                //Console.WriteLine("GetData foreach " + s);
                await GClient.parsTescoPageAsync(cd, s);
            }
            DateTime t2 = DateTime.Now;


            cd = new ConcurrentDictionary<string, Dictionary<string, string>>();
            foreach (string s in GClient.GetDataLazy(cd, "uwodorniony"))
            {
                //Console.WriteLine("GetDataLazy foreach " + s);
                await GClient.parsTescoPageAsync(cd, s);
            }
            DateTime t3 = DateTime.Now;

            // Task.WaitAll(t1, t2, t3);
            //Task.WaitAll( t2);

            Console.WriteLine("nie lazy: " + (t2 - t1).TotalSeconds);
            Console.WriteLine("lazy: " + (t3 - t2).TotalSeconds);



             t1 = DateTime.Now;
            cd = new ConcurrentDictionary<string, Dictionary<string, string>>();
            
                 GClient.parsTescoPageGetData(GClient.GetData(cd, "uwodorniony"), cd);
            
             t2 = DateTime.Now;


            cd = new ConcurrentDictionary<string, Dictionary<string, string>>();
            
            GClient.parsTescoPageGetData(GClient.GetDataLazy(cd, "uwodorniony"),cd);
            
             t3 = DateTime.Now;

            // Task.WaitAll(t1, t2, t3);
            //Task.WaitAll( t2);

            Console.WriteLine("nie lazy: " + (t2 - t1).TotalSeconds);
            Console.WriteLine("lazy: " + (t3 - t2).TotalSeconds);

            //Console.WriteLine(cd.Keys.Count);
            var tmp = cd.OrderBy(x => x.Key).ToList();
            GClient.CreateCsv(tmp);
        }
    }
}
