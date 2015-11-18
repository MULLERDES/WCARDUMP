using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;
namespace CarDumpApp.Controllers
{
    public class PersonalOfficeController :Controller
    {
        private CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();

        // GET: PersonalOffice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateRecord()
        {
            ViewBag.AutoModelId = new SelectList(db.AutoModels, "Id", "Name");
            ViewBag.AutoBrandId = new SelectList(db.AutoBrands, "Id", "Name");
            ViewBag.MemoryTypeID = new SelectList(db.MemoryTypes, "Id", "Name");
            ViewBag.ModuleID = new SelectList(db.Modules, "Id", "Name");


            return View();
        }



 
       

       


        public ActionResult Info(int? brandname, int? modelname)
        {
            List<AutoBrand> brands = null;
            List<AutoModel> models = null;
            List<CarDump> cd = null;
            AutoModel selectedModel = null;
            AutoBrand selectedBrand = null;

            brands = db.AutoBrands.ToList();

            if(brandname != null && brandname != 0)
            {
                selectedBrand = (from c in db.AutoBrands where c.Id == brandname select c).ToList()?[0];
                models = (from s in db.AutoModels
                          where s.AutoBrandID == brandname
                          select s).ToList();
                if(modelname != null && modelname != 0)
                {
                    selectedModel = (from s in db.AutoModels where s.Id == modelname select s).ToList()?[0];
                    if(selectedModel.AutoBrandID == selectedBrand.Id)
                        cd = (from s in db.CarDumps where s.AutoModelId == modelname select s).ToList();
                }
                else
                {
                    cd = (from s in db.CarDumps where s.AutoModel.AutoBrandID == brandname select s).ToList();
                }
            }
            else
            {

                models = new List<AutoModel>(new AutoModel[] { new AutoModel() { Id = 0, Name = "----" } });
            }
            models.Insert(0, new AutoModel() { Id = 0, Name = "-----" });
            brands.Insert(0, new AutoBrand() { Id = 0, Name = "-----" });

            if(cd == null)
            {
                cd = (from s in db.CarDumps select s).ToList();
            }

            CarDumpSearchViewModel vm = new CarDumpSearchViewModel()
            {
                AutoBrands = new SelectList(brands, "Id", "Name", selectedBrand),
                Models = new SelectList(models, "Id", "Name", selectedModel),
                CarDumps = cd
            };
            return View(vm);
        }
    }
}