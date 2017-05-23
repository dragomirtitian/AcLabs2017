using Game.Mvc.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.Mvc.Controllers
{
    [Authorize]
    public class BuildingsController : Controller
    {
        private GameDbContext gameDataContext;
        public BuildingsController()
        {
            this.gameDataContext = new GameDbContext();
        }
        // GET: Buildings
        public ActionResult Index()
        {
            var user = gameDataContext.Users.Find(User.Identity.GetUserId());

            var city = user.Cities.First();

            return View(city);
        }

        public ActionResult Build(int buildingId)
        {
            return View(new BuidlViewModel
            {
                BuildingId = buildingId,
                ExistingTypes = this.gameDataContext.BuildingTypes
                        .Select(_=> new SelectListItem
                        {
                            Value = _.BuildingTypeId.ToString(),
                            Text = _.Name
                        })
            });
        }



        [HttpPost]
        public ActionResult Build(BuidlViewModel vm)
        {
            var buiding = this.gameDataContext.Buildings.Find(vm.BuildingId);
            buiding.BuildingTypeId = vm.SelectedBuildingTypeId;
            buiding.Level = 1;
            this.gameDataContext.SaveChanges();
            return View("Message", new MessageViewModel
            {
                Message = "Building was created"
            });
        }

        public ActionResult Details(int buildingId)
        {
            var building = this.gameDataContext.Buildings.Find(buildingId);
            return View(building);
        }

        public ActionResult Barracks(int buildingId)
        {
            var building = this.gameDataContext.Buildings.Find(buildingId);
            return View(building);
        }
    }

}