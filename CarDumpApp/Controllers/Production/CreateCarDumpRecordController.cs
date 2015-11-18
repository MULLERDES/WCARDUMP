using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;

namespace CarDumpApp.Controllers.Production
{
    public class CreateCarDumpRecordController : Controller
    {
        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();
        // GET: CreateCarDumpRecord
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            ViewBag.NewBrand = new SelectList(db.AutoBrands, "Id", "Name");
            return View();
        } 

        [HttpPost]
        public string Create(string s)
        {


            return $"OK {s} {Request["DDListAutoBrand"]}";
        }
    }
}