using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Util;
using Business.Model;
using Business;

namespace NewItemChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequestor.Init();

            var items = GumtreeHtmlReader.GetAdvertItems("https://www.gumtree.com.au/s-miscellaneous-goods/perth/cardboard+boxes/k0c18319l3008303");

            //DoDatabaseStuff();
            //VisitPage("https://www.gumtree.com.au/s-miscellaneous-goods/perth/cardboard+boxes/k0c18319l3008303");
            //VisitPage("https://intranet.health.wa.gov.au");
        }

        static void VisitPage(string url)
        {
            Console.WriteLine("Submitting request...");
            
            var response = WebRequestor.Get(url);

            Console.WriteLine("Response received.");
        }

        static void DoDatabaseStuff()
        {
            using (var db = new DatabaseContext())
            {
                //var advertItem = new AdvertItem
                //{
                //    GumtreeAdvertId = Guid.NewGuid().ToString(),
                //    Title = "Moodle",
                //    Description = "Noodle the Moodle for sale",
                //    AdvertDate = DateTime.Now
                //};

                //db.AdvertItems.Add(advertItem);

                var subscriptionItem = new SubscriptionRequest
                {
                    GumtreeURL = "http://google.com",
                    RequestedBy = "HE12345",
                    RequestedDate = DateTime.Now,
                    SubscriptionConfirmed = false,
                    Email = "James.Shin@health.wa.gov.au"
                };

                db.SubscriptionRequests.Add(subscriptionItem);
                db.SaveChanges();
            }
        }
    }
}
