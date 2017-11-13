using FDP.OrderService.Data.Model;
using System.Data.Entity.Migrations;

namespace FDP.OrderService.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<OrderDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(OrderDataContext context)
        {
            context.Users.AddOrUpdate(x => x.Id,
                new User() { Id = 1, Email = "test@test.com" , Username = "UsernameTest" },
                new User() { Id = 2, Email = "test2@test.com" , Username = "UsernameTest2" }
                );

            context.Restaurants.AddOrUpdate(x => x.Id,
                new Restaurant()
                {
                    Id = 1,
                    Code = "BOSCHESE"                    
                },
                new Restaurant()
                {
                    Id = 1,
                    Code = "FIOPIZZA"
                }
                );
        }
    }


    
}
