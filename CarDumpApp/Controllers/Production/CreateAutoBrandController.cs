using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;

namespace CarDumpApp.Controllers.Production
{
    public class CreateAutoBrandController :Controller
    {
        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();
        // GET: CreateAutoBrand
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ModalWindow()
        {
            ViewBag.CurrenBrands = new SelectList(db.AutoBrands, "Id", "Name");
            return PartialView();
        }

        [HttpPost]
        public ActionResult ModalWindow(AutoBrand brand, string mm)
        {
            if(brand is AutoBrand)
            {
                db.AutoBrands.Add(brand);
                db.SaveChanges();

            }

            ViewBag.CurrenBrands = new SelectList(db.AutoBrands, "Id", "Name");
            ViewBag.DDListAutoBrand = new SelectList(db.AutoBrands, "Id", "Name");
            ViewBag.DDListAutoModel = new SelectList(db.AutoModels, "Id", "Name");
            return PartialView("DDListAutoBrand");
        }

        public ActionResult DDListAutoBrand(AutoBrand br, AutoModel mdl)
        {

            AutoBrand sel = null;
            if(mdl is AutoModel)
            {
                sel = mdl.AutoBrand;
            }
            else if(br != null)
                sel = br;

            if(sel == null)
                sel = (from c in db.AutoBrands select c).ToList()[0];

            ViewBag.DDListAutoBrand = new SelectList(db.AutoBrands, "Id", "Name", sel?.Id);
            ViewBag.DDListAutoModel = new SelectList(db.AutoModels.Where(tf => tf.AutoBrandID == sel.Id), "Id", "Name", mdl?.Id);
            return PartialView();
        }



        public ActionResult ModelsByBrand(int? id)
        {
            ViewBag.DDListAutoModel = new SelectList(db.AutoModels.Where(c => c.AutoBrandID == id), "Id", "Name");

            return PartialView();
        }
        public ActionResult ModelsByBrandE(int? id)
        {
            ViewBag.AutoModelID = new SelectList(db.AutoModels.Where(c => c.AutoBrandID == id), "Id", "Name");

            return PartialView();
        }

    }
}