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
        // GET: Game
        public ActionResult Index()
        {
            var user = gameDataContext.Users.Find(User.Identity.GetUserId());
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