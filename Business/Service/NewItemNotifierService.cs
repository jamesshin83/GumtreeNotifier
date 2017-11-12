using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class NewItemNotifierService
    {
        public NewItemNotifierService()
        {

        }

        public void CheckNewItemsFromGumtree(SubscriptionRequest request)
        {
            // Visit URL and get adverts
            var adverts = GumtreeHtmlReader.GetAdvertItems(request.GumtreeListingURL);

            // Check difference
            var existingAdverts = GetExistingAdverts(request.SubscriptionId);

            // Store and notify difference
            var diffAdverts = adverts.Where(x => !(existingAdverts.Select(y => y.GumtreeAdvertId).Contains(x.GumtreeAdvertId)));

            if (diffAdverts.Any())
            {
                // Prep new found adverts
                PrepNewAdverts(request, diffAdverts);

                // Notify subscriber of new items


                // Insert diff adverts into database
                InsertAdverts(diffAdverts);
            }
        }

        private void PrepNewAdverts(SubscriptionRequest request, IEnumerable<AdvertItem> diffAdverts)
        {
            var detected = new DetectedAdvertItem
            {
                DetectedDate = DateTime.Now,
                IsNotified = false,
                SubscriptionRequest = request
            };

            foreach (var advert in diffAdverts)
            {
                advert.DetectedAdvertItem = detected;
            }
        }

        private void InsertAdverts(IEnumerable<AdvertItem> diffAdverts)
        {
            using (var db = new DatabaseContext())
            {
                foreach(var advert in diffAdverts)
                {
                    db.SubscriptionRequests.Attach(advert.DetectedAdvertItem.SubscriptionRequest);
                    db.AdvertItems.Add(advert);
                }

                db.SaveChanges();
            }
        }

        private IList<AdvertItem> GetExistingAdverts(Guid subscriptionId)
        {
            using (var db = new DatabaseContext())
            {
                var adverts = db.AdvertItems
                    .Where(x => x.DetectedAdvertItem.SubscriptionRequest.SubscriptionId == subscriptionId)
                    .ToList();

                return adverts;
            }
        }

        public void CheckAllNewItemsFromGumtree()
        {
            using (var db = new DatabaseContext())
            {
                var subscriptions = db.SubscriptionRequests
                    .Where(x => x.IsActive && x.SubscriptionConfirmed) // Active and Subscription confirmed
                    .Where(y => y.StartDate < DateTime.Now && y.EndDate > DateTime.Now); //Between the active date

                foreach(var subscription in subscriptions)
                {
                    CheckNewItemsFromGumtree(subscription);
                }
            }
        }
    }
}
