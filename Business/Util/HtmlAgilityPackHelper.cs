using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

namespace Business
{
    public static class HtmlAgilityPackHelper
    {
        public static HtmlDocument GetHtmlDocumentFromURL(string url)
        {
            var web = new HtmlWeb();

            NetworkCredential proxyCred = new NetworkCredential(@"he67962", @"Ineedcoffee9", @"HDWA");
            WebProxy proxy = new WebProxy("203.0.172.4:8181", false)
            {
                UseDefaultCredentials = false,
                Credentials = proxyCred
            };

            var doc = web.Load(url, "GET", proxy, proxyCred);

            return doc;
        }
    }
}
