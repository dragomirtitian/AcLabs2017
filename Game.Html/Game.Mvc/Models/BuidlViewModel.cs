using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.Mvc.Models
{

    public class BuidlViewModel
    {
        public int BuildingId { get; set; }
        public IEnumerable<SelectListItem> ExistingTypes { get; set; }

        [Required]
        public int? SelectedBuildingTypeId { get; set; }
    }
}