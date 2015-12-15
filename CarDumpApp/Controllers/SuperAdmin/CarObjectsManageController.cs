using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;

namespace CarDumpApp.Controllers.SuperAdmin
{
    public class CarObjectsManageController :Controller
    {


        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();

        public ActionResult Index()
        {
            return View();
        }

        // GET: CarObjectsManage
        public ActionResult Edit(int? CarObjectId)
        {
            CarObject mdl;
            if(CarObjectId == null)
                mdl = new CarObject();
            else mdl = db.CarObjects.Where(tf => tf.Id == CarObjectId).First();


            ViewBag.TransmissionID = new SelectList(db.TransmissionTypes, "Id", "Name", mdl.TransmissionID);
            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "Id", "Name", mdl.FuelTypeID);
            ViewBag.MetricID = new SelectList(db.MetricTypes, "Id", "Name", mdl.MetricID);
            ViewBag.DDListAutoBrand = new SelectList(db.AutoBrands, "Id", "Name", mdl.AutoModel?.AutoBrandID);


            List<AutoModel> models = new List<AutoModel>(new AutoModel[] { new AutoModel() { Id = 0, Name = "-----" } });
            models.AddRange(db.AutoModels.Where(tf=>tf.Id==mdl.AutoModelID));
            ViewBag.AutoModelID = new SelectList(models, "Id", "Name",mdl.AutoModelID);

            return PartialView(mdl);
        }

        [HttpPost]
        public int Edit(CarObject mdl)
        {

            if(mdl.Id == 0)
            {
                db.CarObjects.Add(mdl);
                db.SaveChanges();
                db.Dispose();
                db = new CarDumpDatabaseEntities();
            }
            else
            {
                db.Entry(mdl).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
                db = new CarDumpDatabaseEntities();

            }
            return mdl.Id;
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