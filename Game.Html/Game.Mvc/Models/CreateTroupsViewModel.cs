using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.Mvc.Models
{

    public class CreateTroupsViewModel
    {
        public int BuildingId { get; set; }
        [Range(1, 100)]
        public int Count { get; set; }
        public IEnumerable<SelectListItem> TroupTypes { get; set; }
        [Required]
        public int? SelectedTroupTypeId { get; set; }

        public IEnumerable<Troup> ExistingTroups { get; set; }
    }
}