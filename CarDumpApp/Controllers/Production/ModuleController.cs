using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;
namespace CarDumpApp.Controllers.Production
{
    public class ModuleController : Controller
    {
        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();
        // GET: Module
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult ModalWindow()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ModalWindow(Module mdl)
        {
            if (mdl is Module)
            {
                db.Modules.Add(mdl);
                db.SaveChanges();
            }
            ViewBag.DDListModule = new SelectList(db.Modules, "Id", "Name");
            return PartialView("DDListModule");
        }
        public ActionResult DDListModule(Module mdl)
        {
            ViewBag.DDListModule = new SelectList(db.Modules, "Id", "Name",mdl?.Id);
            return PartialView();
        } 
    }
}