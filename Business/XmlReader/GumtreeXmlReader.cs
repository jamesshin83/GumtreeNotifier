using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Model;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Net;

namespace Business
{
    public static class GumtreeHtmlReader
    {
        public static IList<AdvertItem> GetAdvertItems(string url)
        {
            List<AdvertItem> advertItems = new List<AdvertItem>();
            
            var doc = HtmlAgilityPackHelper.GetHtmlDocumentFromURL(url);

            //Get div of ad collection
            //var adItemsDiv = doc.DocumentNode.Descendants("div")
            //    .Where(x => x.Attributes["class"].Value == "panel search-results-page__main-ads-wrapper user-ad-collection user-ad-collection--row")
            //    .FirstOrDefault();

            // Get all the ad items
            var adItems = doc.DocumentNode.Descendants("a")
                .Where(x => x.Attributes["class"].Value == "user-ad-row link link--base-color-inherit link--hover-color-none link--no-underline")
                .ToList();

            foreach (var item in adItems)
            {
                var advertItem = new AdvertItem
                {
                    Title = GetAdvertTitle(item),
                    Description = GetAdvertDescription(item),
                    AdvertDate =GetAdvertDate(item),
                    GeneralArea = GetAdvertGeneralArea(item),
                    Suburb = GetAdvertSuburb(item),
                    GumtreeAdvertId = GetGumtreeAdvertId(item)
                };

                advertItems.Add(advertItem);
            }

            return advertItems;
        }

        private static string GetGumtreeAdvertId(HtmlNode item)
        {
            return "title";
        }

        private static string GetAdvertSuburb(HtmlNode item)
        {
            return "suburb";
        }

        private static string GetAdvertGeneralArea(HtmlNode item)
        {
            return "general area";
        }

        private static DateTime GetAdvertDate(HtmlNode item)
        {
            return DateTime.Now;
        }

        private static string GetAdvertDescription(HtmlNode item)
        {
            return "desc";
        }

        private static string GetAdvertTitle(HtmlNode item)
        {
            return "title";
        }
    }
}
