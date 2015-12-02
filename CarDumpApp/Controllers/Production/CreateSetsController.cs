using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;
using Microsoft.AspNet.Identity;
namespace CarDumpApp.Controllers.Production
{
    public class CreateSetsController :Controller
    {
        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();
        // GET: CreateSets
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            var lst = (from s in db.CarDumpSets where s.PostedUserID == id select s).ToList();
            return View(lst);
        }

        public ActionResult IndexPartial()
        {
            string id = User.Identity.GetUserId();
            var lst = (from s in db.CarDumpSets where s.PostedUserID == id select s).ToList();
            return PartialView(lst);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //welcome screen
            return View();
        }

        [HttpPost]
        public int Create(int cardumpitem)
        {
            using(var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    CarDump cdi = (from c in db.CarDumps where c.Id == cardumpitem select c).First();//.ToList()[0];
                    CarDumpSet cds = new CarDumpSet()
                    {
                        ModelId = cdi.AutoModelId,
                        CreatedDate = DateTime.Now,
                        Description = "empty description",
                        Year = cdi.Year,
                        PostedUserID = User.Identity.GetUserId()

                    };
                    db.CarDumpSets.Add(cds);
                    db.SaveChanges();
                    SetItem si = new SetItem()
                    {
                        CarDumpID = cardumpitem,
                        CarDumpSetID = cds.Id
                    };
                    db.SetItems.Add(si);
                    db.SaveChanges();


                    transaction.Commit();
                    return cds.Id;

                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return -1;
                }
            }







        }




        public ActionResult EditSet(int? SetItem)
        {
            CreateSetViewModel mdl = new CreateSetViewModel();
            if(SetItem != null)
            {
                try
                {
                    CarDumpSet set = (from s in db.CarDumpSets where s.Id == SetItem select s).First();
                    mdl.CDSet = set;

                    return View(mdl);
                }
                catch(Exception)
                {
                    return HttpNotFound();
                }

            }
            else return HttpNotFound();


            

           
        }


        [HttpPost]
        public ActionResult EditSet(int? SetItem, int? cditem, string saction)
        {
            CreateSetViewModel mdl = new CreateSetViewModel();
            if(SetItem != null)
            {
                try
                {
                    CarDumpSet set = (from s in db.CarDumpSets where s.Id == SetItem select s).First();

                    switch(saction)
                    {
                        case "add":
                            {
                                var slist = set.SetItems.OrderBy(ts => ts.Id).ToList();
                                if(slist.Find(tf => tf.CarDumpID == cditem) == null)
                                {
                                    SetItem si = new Models.SetItem();
                                    si.CarDumpID = (int)cditem;
                                    si.CarDumpSetID = set.Id;
                                    db.SetItems.Add(si);
                                    db.SaveChanges();
                                    db.Dispose();
                                    db = new CarDumpDatabaseEntities();
                                    return EditSet(SetItem, 0, "");
                                }
                                break;
                            }

                        case "rem":
                            {
                                //   List<SetItem> SI = set.SetItems.Where(s => s.Id == cditem).ToList();

                                //MemoryType memoryType = db.MemoryTypes.Find(id);
                                //db.MemoryTypes.Remove(memoryType);
                                //db.SaveChanges();
                                //return RedirectToAction("Index");

                                SetItem SI = db.SetItems.Find(cditem);
                                db.SetItems.Remove(SI);
                                db.SaveChanges();



                                db.Dispose();
                                db = new CarDumpDatabaseEntities();
                                return EditSet(SetItem, 0, "");
                            }
                            
                        default: break;
                    }

                    mdl.CDSet = set;
                    return PartialView("EditSetPartial", mdl);
                }
                catch(Exception)
                {
                    return HttpNotFound();
                }

            }
            else return HttpNotFound();

        }

    }
}