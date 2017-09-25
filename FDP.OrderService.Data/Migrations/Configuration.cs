﻿using System.Data.Entity.Migrations;

namespace FDP.OrderService.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<OrderDataContext>
        {
            public Configuration()
            {
                AutomaticMigrationsEnabled = true;
                AutomaticMigrationDataLossAllowed = true;
            }

            protected override void Seed(OrderDataContext context) { }
        }
    
}