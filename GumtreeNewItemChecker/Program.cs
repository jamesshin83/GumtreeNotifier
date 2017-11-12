using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Util;
using Business.Model;
using Business;
using Business.Service;

namespace NewItemChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequestor.Init();
            ProgramInit();

            NewItemNotifierService newItemService = new NewItemNotifierService();
            newItemService.CheckAllNewItemsFromGumtree(); 

            //var items = GumtreeHtmlReader.GetAdvertItems("https://www.gumtree.com.au/s-miscellaneous-goods/perth/cardboard+boxes/k0c18319l3008303");
            //InsertAdvertItems(items);

            //DoDatabaseStuff();
            //VisitPage("https://www.gumtree.com.au/s-miscellaneous-goods/perth/cardboard+boxes/k0c18319l3008303");
            //VisitPage("https://intranet.health.wa.gov.au");
        }

        private static void ProgramInit()
        {
            using (var db = new DatabaseContext())
            {
                if(!db.SubscriptionRequests.Where(x => x.Email == "jamesshin83@hotmail.com").Any())
                {
                    db.SubscriptionRequests.Add(new SubscriptionRequest
                    {
                        StartDate = DateTime.Parse("1 Nov 2017"),
                        EndDate = DateTime.Now.AddDays(100),
                        GumtreeListingURL = "https://www.gumtree.com.au/s-miscellaneous-goods/perth/cardboard+boxes/k0c18319l3008303",
                        Email = "jamesshin83@hotmail.com",
                        IsActive = true,
                        RequestedDate = DateTime.Now,
                        SubscriptionConfirmed = true,
                        SubscriptionConfirmedDate = DateTime.Now
                    });

                    db.SaveChanges();
                }
            }
        }

        private static void InsertAdvertItems(IList<AdvertItem> items)
        {
            using (var db = new DatabaseContext())
            {
                db.AdvertItems.AddRange(items);
                db.SaveChanges();
            }
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
                    GumtreeListingURL = "http://google.com",
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
