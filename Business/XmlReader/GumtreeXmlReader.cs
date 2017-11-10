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
                    Price = GetAdvertPrice(item),
                    AdvertUrl = GetAdvertUrl(item),
                    GeneralArea = GetAdvertGeneralArea(item),
                    Suburb = GetAdvertSuburb(item),
                    GumtreeAdvertId = GetGumtreeAdvertId(item)
                };

                advertItems.Add(advertItem);
            }

            return advertItems;
        }

        private static string GetAdvertUrl(HtmlNode item)
        {
            var url = string.Format("{0}{1}", "http://www.gumtree.com.au", item.Attributes["href"].Value);

            return url;
        }

        private static string GetAdvertPrice(HtmlNode item)
        {
            var priceNode = item.Descendants("span")
                .Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "user-ad-price__price")
                .FirstOrDefault();

            return (priceNode != null) ? priceNode.InnerText : "No price";
        }

        private static string GetGumtreeAdvertId(HtmlNode item)
        {
            var id = item.Attributes["href"].Value.Split(new char[] { '/' }).Last();

            return id;
        }

        private static string GetAdvertSuburb(HtmlNode item)
        {
            var suburbNode = item.Descendants("span")
                .Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "user-ad-row__location")
                .FirstOrDefault();

            return (suburbNode != null) ? suburbNode.InnerText : "No suburb";
        }

        private static string GetAdvertGeneralArea(HtmlNode item)
        {
            var areaNode = item.Descendants("span")
                .Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "user-ad-row__location-area")
                .FirstOrDefault();

            return (areaNode != null) ? areaNode.InnerText : "No general area";
        }

        private static string GetAdvertDescription(HtmlNode item)
        {
            var descNode = item.Descendants("p")
                .Where(x => x.Attributes["id"] != null && x.Attributes["id"].Value.StartsWith("user-ad-desc-MAIN-"))
                .FirstOrDefault();

            return (descNode != null) ? descNode.InnerText : "No description";
        }

        private static string GetAdvertTitle(HtmlNode item)
        {
            var titleNode = item.Descendants("p")
                .Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "user-ad-row__title")
                .FirstOrDefault();

            return (titleNode != null) ? titleNode.InnerText : "No title";
        }
    }
}
