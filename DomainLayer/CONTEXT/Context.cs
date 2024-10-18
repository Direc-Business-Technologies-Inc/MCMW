using DomainLayer.SAO_DATABASE;
using System.Data.Entity;

namespace DomainLayer.CONTEXT
{
    public class Context : DbContext
    {
        public Context() : base("name=Addon")
        {
            Database.SetInitializer<Context>(new CreateDatabase());
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<OneTimeWeigh> OneTimeWeigh { get; set; }
        public DbSet<UserDefined> UserDefined { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<TruckWeight> TruckWeight { get; set; }
        public DbSet<BinNo> BinNo { get; set; }
        public DbSet<TruckFleet> TruckFleet { get; set; } 
        public DbSet<Price> Price { get; set; } 


        //public DbSet<Document_Lines> Document_Lines { get; set; }

        public class CreateDatabase : CreateDatabaseIfNotExists<Context>
        {
            protected override void Seed(Context context)
            {
                base.Seed(context);
            }
        }
    }
}