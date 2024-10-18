using System.Data.Entity.Migrations;
using System.Linq;
using DomainLayer.CONTEXT;
using DomainLayer.SAO_DATABASE;
namespace DomainLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        //protected override void Seed(Context context)
        //{
        //    //  This method will be called after migrating to the latest version.

        //    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //    //  to avoid creating duplicate seed data.

        //    //if (!context.Users.Any())
        //    //{
        //    //    var model = new Users();
        //    //    model.Username = "Direc";
        //    //    model.Password = "B1Admin";
        //    //    context.Users.Add(model);
        //    //    context.SaveChanges();
        //    //}

        //}
    }
}
