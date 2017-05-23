using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Game.Mvc.Models;

namespace Game.Mvc.Controllers
{
    public class CitiesController : Controller
    {
        private GameDbContext db = new GameDbContext();

        // GET: ApplicationUsers
        public ActionResult Index(CityFilterViewModel filterModel)
        {
            IQueryable<City> query = db.Cities;
            if(filterModel.Name != null)
            {
                query = query.Where(u => u.ApplicationUser.UserName.Contains(filterModel.Name));
            }

            if (filterModel.Email != null)
            {
                query = query.Where(u => u.ApplicationUser.Email.Contains(filterModel.Email));
            }
            
            if(filterModel.MinTroupCount != null)
            {
                query = query.Where(c => c.Troups.Sum(t => t.TroupCount) >= filterModel.MinTroupCount);
            }

            if (filterModel.MaxTroupCount != null)
            {
                query = query.Where(c => c.Troups.Sum(t => t.TroupCount) <= filterModel.MaxTroupCount);
            }
            
            filterModel.Results = query.ToList();

            return View(filterModel);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
