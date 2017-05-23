namespace Game.Mvc.Migrations
{
    using Game.Mvc.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Game.Mvc.Models.GameDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Game.Mvc.Models.GameDbContext";
        }

        protected override void Seed(Game.Mvc.Models.GameDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.BuildingTypes.AddOrUpdate(
                p => p.Name,
                new BuildingType { Name = "Garnary" },
                new BuildingType { Name = "Barn" },
                new BuildingType { Name = "Barracks" }
            );

            foreach (var city in context.Cities.ToList())
            {
                for (int i = 0; i < 13; i++)
                {
                    var building = city.Buildings.ElementAtOrDefault(i);
                    if(building == null)
                    {
                        building = new Building { City = city };
                        city.Buildings.Add(building);
                    }
                    // Add other building changes here 
                }
            }
        }
    }
}
