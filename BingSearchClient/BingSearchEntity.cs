using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingSearcher
{
  public  class BingSearchEntity
    {
        public BingSearchEntity(string Description, string StringUrl)
        {
            this.StringUrl = StringUrl;
            this.Description = Description; 
        }

        public string StringUrl { get; private set; }
        public string Description { get; private set; }

        public override string ToString()
        {
            return $"{Description}\n{StringUrl}";
        }
    }
}
