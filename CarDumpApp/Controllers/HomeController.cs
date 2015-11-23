using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;

namespace CarDumpApp.Controllers
{
    public class HomeController :Controller
    {
        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();
        public ActionResult Index()
        {
            return RedirectToAction("Search");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Search(int? brandname, int? modelname)
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
            models.Insert(0, new AutoModel() { Id = 0, Name = "All models" });
            brands.Insert(0, new AutoBrand() { Id = 0, Name = "All auto" });

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

        //[HttpPost]
        //public ActionResult SearchP(int? brandname, int? modelname)
        //{
        //    var models = (from c in db.AutoModels where c.AutoBrandID == 1 select c).ToList();
        //    return PartialView("SearchP", new SelectList(models, "Id", "Name"));
        //}


        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDump cd = (from s in db.CarDumps where s.Id == id select s).ToList()[0];
            if(cd == null)
            {
                return HttpNotFound();
            }
            return View(cd);
        }


        public FileContentResult FileDownload(int parentid)
        {
            //declare byte array to get file content from database and string to store file name
            byte[] fileData;
            string fileName;
            //create object of LINQ to SQL class
            var filedb = (from s in db.StoredFiles where s.ParentCarDumpRecordId == parentid select s).ToList();
            if(filedb.Count > 0)
            {
                fileData = filedb[filedb.Count - 1].FileData;
                fileName = filedb[filedb.Count - 1].OriginalFileName;
                return File(fileData,"car dump" , fileName);
            }
            else return null;
            //only one record will be returned from database as expression uses condtion on primary field
            //so get first record from returned values and retrive file content (binary) and filename
         
            //return file and provide byte file content and file name
           
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
                CD = (from s in db.CarDumps where s.PostedUserID == userId && s.AccessLevelID==accessId select s).ToList();
            }
            
         
            return PartialView(CD);
        }

    }
}