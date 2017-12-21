using System.Collections.Generic;

namespace BingSearcher
{
  public interface IBingSearcherClient
  {
     List<string> GetUrlsList();
     List<BingSearchEntity> GetBingSearchEntityList();

     IEnumerable<BingSearchEntity> GetBingSearchEntityLazyList();

     IEnumerable<string> GetUrlsLazyList();

  }
}