using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Model;

namespace Business
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=GumtreeDb")
        {

        }
        public DbSet<AdvertItem> AdvertItems { get; set; }
        public DbSet<DetectedAdvertItem> DetectedAdvertItems { get; set; }
        public DbSet<SubscriptionRequest> SubscriptionRequests { get; set; }
    }
}
