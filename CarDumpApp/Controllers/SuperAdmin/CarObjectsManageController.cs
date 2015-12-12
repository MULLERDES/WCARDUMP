using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;

namespace CarDumpApp.Controllers.SuperAdmin
{
    public class CarObjectsManageController : Controller
    {


        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();
        
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: CarObjectsManage
        public ActionResult Edit(int? CarObjectId)
        {
            CarObject mdl = new CarObject();

            //     ViewBag.AutoBrandID = new SelectList(db.AutoBrands, "Id", "Name", autoModel.AutoBrandID);
            ViewBag.TransmissionID = new SelectList(db.TransmissionTypes, "Id", "Name");
            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "Id", "Name");
            ViewBag.MetricID = new SelectList(db.MetricTypes, "Id", "Name");
            return PartialView();
        }

        [HttpPost]
        public int Edit( CarObject mdl)
        {

            return 0;
        }

        //public ActionResult Edit([Bind(Include = "Id,Name,AutoBrandID")] AutoModel autoModel)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        db.Entry(autoModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.AutoBrandID = new SelectList(db.AutoBrands, "Id", "Name", autoModel.AutoBrandID);
        //    return View(autoModel);
        //}

    }
}