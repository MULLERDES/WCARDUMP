using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;
namespace CarDumpApp.Controllers.Production
{
    public class MemoryTypeController : Controller
    {
        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();
        // GET: MemoryType
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
        public ActionResult ModalWindow(MemoryType mem)
        {
            if(mem is MemoryType)
            {
                db.MemoryTypes.Add(mem);
                db.SaveChanges();
            }
            ViewBag.DDListMemory = new SelectList(db.MemoryTypes, "Id", "Name");
            return PartialView("DDListMemory");
        }
        public ActionResult DDListMemory()
        {
            ViewBag.DDListMemory = new SelectList(db.MemoryTypes, "Id", "Name");
            return PartialView();
        }
    }
}