using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Game.Mvc.Models
{
    public partial class GameDbContext // 
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Mine> Mines { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }

        public DbSet<TroupType> TroupTypes { get; set; }
        public DbSet<Troup> Troups { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }

        public virtual IList<Mine> Mines { get; set; }

        public virtual IList<Troup> Troups { get; set; }

        public virtual IList<Building> Buildings { get; set; }

        public virtual IList<Resource> Resources { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class Resource
    {
        public int ResourceId { get; set; }

        public int CityId { get; set; }

        [ScriptIgnore]
        public virtual City City { get; set; }

        public DateTime LastUpdate { get; set; }
        public ResourceType Type { get; set; }

        public double Level { get; set; }
    }

    public class Mine
    {
        public string MineStyle { get; set; }

        public int MineId { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public int Level { get; set; }

        public ResourceType Type { get; set; }

        public DateTime? UpgradeCompletedAt { get; set; }

        public double GetProductionPerHour(int? level = null)
        {
            return (level ?? this.Level) * 13; 
        }

        public (int amount, ResourceType type)[] GetUpgradeRequirements()
        {
            return new[]
            {
                (10 * (Level+ 1), ResourceType.Clay),
                (10 * (Level+ 1), ResourceType.Iron),
                (10 * (Level+ 1), ResourceType.Wheat),
                (10 * (Level+ 1), ResourceType.Wood),
            };
        }
    }

    public enum ResourceType
    {
        Wheat,
        Iron,
        Clay,
        Wood
    }


    public class Building
    {
        public int BuildingId { get; set; }
        public int Level { get; set; }
        public int? BuildingTypeId { get; set; }
        public virtual BuildingType BuildingType { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }

    public class BuildingType
    {
        public int BuildingTypeId { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Troup
    {
        public int TroupId { get; set; }
        public int TroupTypeId { get; set; }
        public virtual TroupType TroupType { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int TroupCount { get; set; }
        
    }

    public class TroupType
    {
        public int TroupTypeId { get; set; }
        public string Name { get; set; }
        public double Attack { get; set; }
        public double Defence { get; set; }
        public int CreationSpeed { get; set; }
    }
}