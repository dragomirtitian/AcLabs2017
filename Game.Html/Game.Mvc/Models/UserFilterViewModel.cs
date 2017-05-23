using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Mvc.Models
{
    public class CityFilterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? MinTroupCount { get; set; }
        public int? MaxTroupCount { get; set; }

        public List<City> Results { get; set; }
    }
}