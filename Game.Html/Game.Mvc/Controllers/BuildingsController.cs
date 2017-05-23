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
            return View(new CreateTroupsViewModel
            {
                BuildingId =  buildingId,
                ExistingTroups = building.City.Troups,
                TroupTypes = this.gameDataContext.TroupTypes
                    .Select(t=> new SelectListItem
                    {
                        Text = t.Name,
                        Value = t.TroupTypeId.ToString()
                    })
                    .ToList()
            });
        }

        [HttpPost]
        public ActionResult Barracks(CreateTroupsViewModel create)
        {
            var building = this.gameDataContext.Buildings.Find(create.BuildingId);
            var city = building.City;
            var type = this.gameDataContext.TroupTypes.Find(create.SelectedTroupTypeId);

            var resourceNeeds = city.Resources
                .Select(_ => (res: _, needed: create.Count * (type.Attack + type.Defence) / 100));

            if(resourceNeeds.Any(_=> _.res.Level < _.needed))
            {
                return View("Message", new MessageViewModel
                {
                    Message = "Not enogh resources"
                });
            }

            foreach (var item in resourceNeeds)
            {
                item.res.Level -= item.needed;
            }

            var troup = city.Troups.FirstOrDefault(t => t.TroupTypeId == create.SelectedTroupTypeId);
            if(troup == null)
            {
                this.gameDataContext.Troups.Add(troup = new Troup
                {
                    TroupTypeId = create.SelectedTroupTypeId.Value,
                    CityId = city.CityId
                });
            }

            troup.TroupCount += create.Count;
            this.gameDataContext.SaveChanges();

            return this.Barracks(create.BuildingId);
        }
    }

}