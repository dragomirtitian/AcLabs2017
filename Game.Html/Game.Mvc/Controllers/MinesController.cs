using Game.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Game.Mvc.Controllers
{
    [Authorize]
    public class MinesController : Controller
    {
        private GameDbContext gameDataContext;
        public MinesController()
        {
            this.gameDataContext = new GameDbContext();
        }

        private void UpdteResources(City city)
        {
            var start = DateTime.Now;
            foreach (var res in city.Resources)
            {
                foreach (var mine in city.Mines)
                {
                    if (mine.Type == res.Type)
                    {
                        res.Level += mine.GetProductionPerHour() * (start - res.LastUpdate).TotalHours;
                    }
                }
                res.LastUpdate = start;
            }
            gameDataContext.SaveChanges();

        }
        [HttpGet]
        public ActionResult Resources()
        {
            var user = gameDataContext.Users.Find(User.Identity.GetUserId());

            var city = user.Cities.First();
            this.UpdteResources(city);

            return this.Json(city.Resources.Select(_=> new { Type = _.Type.ToString(), _.Level }).ToArray(), JsonRequestBehavior.AllowGet);
        }

        // GET: Game
        public ActionResult Index()
        {
            var user = gameDataContext.Users.Find(User.Identity.GetUserId());

            var city = user.Cities.First();
            this.UpdteResources(city);
            return View(user.Cities.First());
        }
        public ActionResult Details(int mineId)
        {
            var mine = this.gameDataContext.Mines.Find(mineId);
            return View(mine);
        }

        [HttpPost]
        public ActionResult Upgrade(int mineId)
        {
            var mine = this.gameDataContext.Mines.Find(mineId);
            mine.Level = mine.Level + 1;
            this.gameDataContext.SaveChanges();
            return View(new MessageViewModel
            {
                Message = "Upgrade succesful"
            });
        }
    }
}