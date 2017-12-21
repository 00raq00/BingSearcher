using System;
using System.Collections.Generic;

namespace BingSearcher
{
  public class BingSearcherClient : IBingSearcherClient
  {
    private BingSearchParameters bingSearchParameters;
    private string searchedString;
    
    public BingSearcherClient(BingSearchParameters bingSearchParameters, string searchedString)
    {
      this.bingSearchParameters = bingSearchParameters ?? throw new ArgumentNullException(nameof(bingSearchParameters));
      this.searchedString = searchedString ?? throw new ArgumentNullException(nameof(searchedString));
    }

    public IEnumerable<BingSearchEntity> GetBingSearchEntityLazyList()
    {
#pragma warning disable CS0612 // Type or member is obsolete
      return BingSearchClient.GetBingSearchEntityLazyList(searchedString, bingSearchParameters);
#pragma warning restore CS0612 // Type or member is obsolete
    }

    public List<BingSearchEntity> GetBingSearchEntityList()
    {
#pragma warning disable CS0612 // Type or member is obsolete
      return BingSearchClient.GetBingSearchEntityList(searchedString, bingSearchParameters);
#pragma warning restore CS0612 // Type or member is obsolete
    }

    public IEnumerable<string> GetUrlsLazyList()
    {
#pragma warning disable CS0612 // Type or member is obsolete
      return BingSearchClient.GetUrlsLazyList(searchedString, bingSearchParameters);
#pragma warning restore CS0612 // Type or member is obsolete
    }

    public List<string> GetUrlsList()
    {
#pragma warning disable CS0612 // Type or member is obsolete
      return BingSearchClient.GetUrlsList(searchedString, bingSearchParameters);
#pragma warning restore CS0612 // Type or member is obsolete
    }
  }
}