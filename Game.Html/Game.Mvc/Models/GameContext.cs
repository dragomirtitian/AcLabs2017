using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Game.Mvc.Models
{
    public partial class GameDbContext 
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Mine> Mines { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }

        public virtual IList<Mine> Mines { get; set; }
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
        public int MineId { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public int Level { get; set; }

        public ResourceType Type { get; set; }

        public double GetProductionPerHour(int? level = null)
        {
            return (level ?? this.Level) * 13; 
        }
    }

    public enum ResourceType
    {
        Wheat,
        Iron,
        Clay,
        Wood
    }
}