using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace BingSearcher
{
  [Obsolete]
  public class BingSearchClient
  {
    private static string urlBase = "http://www.bing.com";
    

    public static List<string> GetUrlsList(string searchedString, BingSearchParameters bingSearchParameters = null)
    {
      return GetUrlsLazyList(searchedString, bingSearchParameters).ToList();
    }
    public static List<BingSearchEntity> GetBingSearchEntityList(string searchedString, BingSearchParameters bingSearchParameters = null)
    {
      return GetBingSearchEntityLazyList(searchedString, bingSearchParameters).ToList();
    }

    public static IEnumerable<BingSearchEntity> GetBingSearchEntityLazyList(string searchedString, BingSearchParameters bingSearchParameters = null)
    {

      SetDefaultNetworkCredentials(bingSearchParameters?.UseDefaultNetworkCredentials);

      HtmlNodeCollection htmlNodeCollection;

      var searchUrl = SearchUrl(searchedString, bingSearchParameters);

      do
      {
        HtmlDocument doc = null;
        try
        {
          doc = new HtmlWeb().LoadFromBrowser(urlBase + searchUrl);
        }
        catch (Exception e)
        {
          doc = new HtmlWeb().Load(urlBase + searchUrl);
        }

        var rowsNodes = doc.DocumentNode.SelectNodes("//ol[@id='b_results']//h2/a");

        var bNoCount = doc.DocumentNode.SelectNodes("//ol[@id='b_results']//li")?.Count;

        if (rowsNodes != null)
        {
          foreach (HtmlNode row in rowsNodes)
          {
            InvokeAction(bingSearchParameters, $"{row.InnerText}  {row.Attributes["href"].Value}");

            yield return new BingSearchEntity(row.InnerText, row.Attributes["href"].Value);
          }

          htmlNodeCollection = doc.DocumentNode.SelectNodes("//a[@class='sb_pagN']");
          if (htmlNodeCollection != null)
          {
            searchUrl =
                htmlNodeCollection.Last().Attributes["href"].Value.Replace("&amp;", "&")
                    .Replace("%3a", "%3A");
          }
        }
        else if (bNoCount.HasValue)
        {
          htmlNodeCollection = null;
        }
        else
        {
          InvokeAction(bingSearchParameters, $"Unsupported responce from Bing for searchedString {searchedString}");
          throw new Exception($"Unsupported responce from Bing for searchedString {searchedString}");
        }
      }
      while (htmlNodeCollection != null);

    }

    public static IEnumerable<string> GetUrlsLazyList(string searchedString, BingSearchParameters bingSearchParameters = null)
    {
      return GetBingSearchEntityLazyList(searchedString, bingSearchParameters).Select(x => x.StringUrl);
    }

    private static void InvokeAction(BingSearchParameters bingSearchParameters, string st)
    {
      bingSearchParameters?.LogAction?.Invoke(st);
    }

    private static string SearchUrl(string searchedString, BingSearchParameters bingSearchParameters)
    {
      if (searchedString.Length > 2000)
      {
        var formattableString = $"searchedString is too long. Max Length = 2000, current length {searchedString.Length}";
        InvokeAction(bingSearchParameters, formattableString);
        throw new Exception(formattableString);
      }
      bool searchAsOneString = bingSearchParameters?.SearchAsOnePhrase != null && (bingSearchParameters.SearchAsOnePhrase);
      string searchOverPage = bingSearchParameters?.SearchOverPage;
      if (searchedString.ToLower().Contains("site:"))
      {
        var formattableString = $"Unsupported search string. SearchedString cannot contains 'site:'. Current searchedString: {searchedString}";
        InvokeAction(bingSearchParameters, formattableString);
        throw new Exception(formattableString);
      }

      string searchUrl = "/search?q=" +
          (String.IsNullOrWhiteSpace(searchOverPage) ? "" : WebUtility.HtmlEncode("site:" + searchOverPage + " ")) +
          (searchAsOneString ? (@"""") : "") + WebUtility.HtmlEncode(searchedString) +
                         (searchAsOneString ? (@"""") : "") + "&first=1";
      return searchUrl;
    }

    internal static void SetDefaultNetworkCredentials(bool? defaultNetworkCredentials)
    {
      if (defaultNetworkCredentials.HasValue && defaultNetworkCredentials.Value)
      {
        WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
      }
    }
  }
}
