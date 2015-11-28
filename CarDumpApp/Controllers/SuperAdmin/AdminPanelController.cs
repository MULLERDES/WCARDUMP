using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;

namespace CarDumpApp.Controllers.SuperAdmin
{
 
    [Authorize]
    public class AdminPanelController : Controller
    {
        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();
        // GET: AdminPanel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FullMenu()
        {
            return PartialView();
        }

        public ActionResult CarDumpsListByUserId(string userId, int? accessId)
        {
            IEnumerable<CarDump> CD = null;// (from s in db.CarDumps where s.PostedUserID == userId select s).ToList();
            if(accessId == null)
            {
                CD = (from s in db.CarDumps where s.PostedUserID == userId select s).ToList();
            }
            else
            {
                CD = (from s in db.CarDumps where s.PostedUserID == userId && s.AccessLevelID == accessId select s).ToList();
            }


            return PartialView(CD);
        }
    }
}