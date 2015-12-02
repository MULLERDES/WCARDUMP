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
            return View();
            //return RedirectToAction("Search");
        }

        public ActionResult CompleteSets()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetModelsByBrand(int? idbr)
        {
            List<AutoModel> itms = new List<AutoModel>();
            if(idbr != null)
                itms.AddRange(db.AutoModels.Where(c => c.AutoBrandID == idbr).OrderBy(tf => tf.Name).ToArray());
            itms.Insert(0, new AutoModel() { Id = 0, Name = "Model" });
            ViewBag.ModelsByBrand = new SelectList(itms, "Id", "Name");
            return PartialView();
        }

        [HttpGet]
        public ActionResult GetAllBrands()
        {
            List<AutoBrand> itms = db.AutoBrands.OrderBy(tf => tf.Name).ToList();
            itms.Insert(0, new AutoBrand() { Id = 0, Name = "Brand" });
            ViewBag.AllBrands = new SelectList(itms, "Id", "Name");
            return PartialView();
        }

        [HttpGet]
        public ActionResult GetAllModules()
        {
            List<Module> itms = db.Modules.OrderBy(tf => tf.Name).ToList();
            itms.Insert(0, new Module() { Id = 0, Name = "Module" });
            ViewBag.AllModules = new SelectList(itms, "Id", "Name");
            return PartialView();
        }
        [HttpGet]
        public ActionResult GetAllMemoryTypes()
        {
            List<MemoryType> itms = db.MemoryTypes.OrderBy(tf => tf.Name).ToList();
            itms.Insert(0, new MemoryType() { Id = 0, Name = "MemoryType" });
            ViewBag.AllMemoryTypes = new SelectList(itms, "Id", "Name");
            return PartialView();
        }

        [HttpGet]
        public ActionResult SearchResult(int? page, int? brandid, int? modelid, int? moduleid, int? memoryid)
        {
            SearchResultHomeVM mdl = new SearchResultHomeVM();

            int perpageCount = SearchResultHomeVM.PerPageNumber;
            List<CarDump> RetValue = new List<CarDump>();

            RetValue.AddRange(
                db.CarDumps.Where(tf => ((modelid == null) ? ((brandid == null) ? true : (brandid == 0 ? true : tf.AutoModel.AutoBrandID == brandid)) : (modelid == 0 ? (brandid == null ? true : tf.AutoModel.AutoBrandID == brandid) : tf.AutoModelId == modelid)) &&
                ((moduleid == null) ? true : tf.ModuleID == moduleid) &&
                (tf.Checked == true) &&
                (tf.AccessLevelID == 1) &&
                ((memoryid == null) ? true : tf.MemoryTypeID == memoryid)
                                ).OrderByDescending(tf => tf.Id).Skip((page == null) ? 0 : ((int)page) * perpageCount).Take(perpageCount).ToArray()
                );

            mdl.Cardumps = RetValue;
            mdl.CurrentPage = page == null ? 0 : (int)page;
            return PartialView(mdl);
        }

        [HttpGet]
        public ActionResult SearchResultSets(int? page, int? brandid, int? modelid, int? year)
        {
            SearchSetsViewModel mdl = new SearchSetsViewModel();

            int perpageCount = SearchSetsViewModel.PerPageNumber;
            List<CarDumpSet> RetValue = new List<CarDumpSet>();

            RetValue.AddRange(
                db.CarDumpSets.Where(tf =>
                ((modelid == null) ? ((brandid == null) ? true : (brandid == 0 ? true : tf.AutoModel.AutoBrandID == brandid)) : (modelid == 0 ? (brandid == null ? true : tf.AutoModel.AutoBrandID == brandid) : tf.ModelId == modelid)) &&
                ((year == null) ? true : tf.Year == year)
                ).OrderByDescending(tf => tf.Id).Skip((page == null) ? 0 : ((int)page) * perpageCount).Take(perpageCount).ToArray()
                );

            mdl.Sets = RetValue;

            mdl.CurrentPage = page == null ? 0 : (int)page;

            return PartialView(mdl);
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
                        cd = (from s in db.CarDumps where s.AutoModelId == modelname && s.AccessLevelID == 1 select s).ToList();
                }
                else
                {
                    cd = (from s in db.CarDumps where s.AutoModel.AutoBrandID == brandname && s.AccessLevelID == 1 select s).ToList();
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
                cd = (from s in db.CarDumps where s.AccessLevelID == 1 select s).ToList();
            }

            CarDumpSearchViewModel vm = new CarDumpSearchViewModel()
            {
                AutoBrands = new SelectList(brands, "Id", "Name", selectedBrand),
                Models = new SelectList(models, "Id", "Name", selectedModel),
                CarDumps = cd
            };
            return View(vm);
        }




        public ActionResult Details(int? id)
        {
            try
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
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }


        public ActionResult SetDetails(int? id)
        {
            try
            {
                if(id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CardumpSetDetailsViewModel mdl = new CardumpSetDetailsViewModel();
                mdl.Set = db.CarDumpSets.Where(tf => tf.Id == id).First();
                List<SetItem> SI = db.SetItems.Where(tf => tf.CarDumpSetID == mdl.Set.Id).ToList();

                mdl.CarDumps = new List<CarDump>();
                foreach(var item in SI)
                {
                    mdl.CarDumps.Add(item.CarDump);
                }

              
                return View(mdl);
            }
            catch(Exception)
            {
                return HttpNotFound();
            }
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
                return File(fileData, "car dump", fileName);
            }
            else return null;
            //only one record will be returned from database as expression uses condtion on primary field
            //so get first record from returned values and retrive file content (binary) and filename

            //return file and provide byte file content and file name

        }


        //view search


        //partial view result

        [HttpGet]
        public int NumerOfAllCarDumps()
        {
            //select COUNT(*) from CarDumps


            return db.CarDumps.Count(c=>c.Checked==true);
        }

    }
}