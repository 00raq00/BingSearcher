using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Web;

namespace BingSearcher
{
    public class BingSearchParameters
    {
        public bool? UseDefaultNetworkCredentials;
        public string SearchOverPage;
        public bool SearchAsOnePhrase;
        public LogActionString LogAction;

        public delegate void LogActionString(string s);
    }
}
