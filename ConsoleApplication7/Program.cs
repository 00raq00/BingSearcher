using BingSearcher;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Program
  {
    [STAThread]
    static void Main(string[] args)
        {
            ConcurrentDictionary<string, Dictionary<string, string>> cd = new ConcurrentDictionary<string, Dictionary<string, string>> ();

      //Task.WaitAll(GetValue());
      /* var bng = new BingSearchParameters()
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
       */

    BingSearchParameters.LogActionString @string = Console.WriteLine;

      foreach (var url in BingSearchClient.GetUrlsList("usb3.0 dysk ", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "allegro.pl", LogAction = @string }))
      {
        Console.WriteLine(url);
      }
      foreach (var url in BingSearchClient.GetUrlsLazyList("usb3.0 dysk ", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "allegro.pl", LogAction = @string }))
      {
        Console.WriteLine(url);
      }

      GetValue();


            Console.Read();
        }

        private static void GetValue()
        {

      ConcurrentDictionary<string, Dictionary<string, string>> cd;
            var gClient = new GClient();
            
            DateTime t1 = DateTime.Now;
            cd = new ConcurrentDictionary<string, Dictionary<string, string>>();
      List<string> list = BingSearchClient.GetUrlsList("uwodorniony", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
            {
                //Console.WriteLine("GetData foreach " + s);
                gClient.parsTescoPage(cd, s);
            }
            DateTime t2 = DateTime.Now;

      list = BingSearchClient.GetUrlsList("utwardzony", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }



      list = BingSearchClient.GetUrlsList("utwardzone", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }



      list = BingSearchClient.GetUrlsList("uwodornione", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }

      var st =   cd.OrderBy(g=> g.Key).Select(x => "<strong>" + x.Key + "</strong>" + "<br/><div style=\"margin-left: 1em; margin-right: 1em; text-align: center; align: center; \"><img border=\"0\" height=\"120\" width=\"120\" src=\"" + x.Value.Where(y => y.Key == "img").Select(z => z.Value).FirstOrDefault().ToString() + "\" class=\"\" style=\"clear: both; text-align: center; display: inline-block;\"></div><br/>" + x.Value.Where(y => y.Key == "ingredients").Select(z => z.Value).FirstOrDefault().ToString()).ToList().Aggregate((c, b) => c + "<br/>"+ "<br/>" + b);

            Console.WriteLine("lazy: " + (t2 - t1).TotalSeconds);




      cd = new ConcurrentDictionary<string, Dictionary<string, string>>();
      list = BingSearchClient.GetUrlsList("olej palmowy", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }
      t2 = DateTime.Now;

      list = BingSearchClient.GetUrlsList("oleje: palmowy", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }



      list = BingSearchClient.GetUrlsList("tłuszcz palmowy", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }



      list = BingSearchClient.GetUrlsList("tłuszcze: palmowy", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }

      st = cd.OrderBy(g => g.Key).Select(x => "<strong>" + x.Key + "</strong>" + "<br/><div style=\"margin-left: 1em; margin-right: 1em; text-align: center; align: center; \"><img border=\"0\" height=\"120\" width=\"120\" src=\"" + x.Value.Where(y => y.Key == "img").Select(z => z.Value).FirstOrDefault().ToString() + "\" class=\"\" style=\"clear: both; text-align: center; display: inline-block;\"></div><br/>" + x.Value.Where(y => y.Key == "ingredients").Select(z => z.Value).FirstOrDefault().ToString()).ToList().Aggregate((c, b) => c + "<br/>" + "<br/>" + b);








      cd = new ConcurrentDictionary<string, Dictionary<string, string>>();
      list = BingSearchClient.GetUrlsList("wzmacniacz smaku", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }
      t2 = DateTime.Now;

      list = BingSearchClient.GetUrlsList("wzmacniacze smaku", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }



      list = BingSearchClient.GetUrlsList("glutaminian monosodowy", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }



      list = BingSearchClient.GetUrlsList("glutaminian sodowy", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }



      list = BingSearchClient.GetUrlsList("e621", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }

      list = BingSearchClient.GetUrlsList("e-621", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }

      list = BingSearchClient.GetUrlsList("e 621", new BingSearchParameters() { UseDefaultNetworkCredentials = true, SearchAsOnePhrase = true, SearchOverPage = "tesco.pl" });
      foreach (string s in list)
      {
        //Console.WriteLine("GetData foreach " + s);
        gClient.parsTescoPage(cd, s);
      }

      st = cd.OrderBy(g => g.Key).Select(x => "<strong>" + x.Key + "</strong>" + "<br/><div style=\"margin-left: 1em; margin-right: 1em; text-align: center; align: center; \"><img border=\"0\" height=\"120\" width=\"120\" src=\"" + x.Value.Where(y => y.Key == "img").Select(z => z.Value).FirstOrDefault().ToString() + "\" class=\"\" style=\"clear: both; text-align: center; display: inline-block;\"></div><br/>" + x.Value.Where(y => y.Key == "ingredients").Select(z => z.Value).FirstOrDefault().ToString()).ToList().Aggregate((c, b) => c + "<br/>" + "<br/>" + b);


      Console.ReadLine();
        }
    }
}
