using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;

namespace CarDumpApp.Controllers.SuperAdmin
{
    public class AutoModelsAdminController : Controller
    {
        private CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();

        // GET: AutoModelsAdmin
        public ActionResult Index()
        {
            var autoModels = db.AutoModels.Include(a => a.AutoBrand).OrderBy(ob=>ob.AutoBrand.Name);
            return View(autoModels.ToList());
        }

        // GET: AutoModelsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoModel autoModel = db.AutoModels.Find(id);
            if (autoModel == null)
            {
                return HttpNotFound();
            }
            return View(autoModel);
        }

        // GET: AutoModelsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.AutoBrandID = new SelectList(db.AutoBrands, "Id", "Name");
            return View();
        }

        // POST: AutoModelsAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AutoBrandID")] AutoModel autoModel)
        {
            if (ModelState.IsValid)
            {
                db.AutoModels.Add(autoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AutoBrandID = new SelectList(db.AutoBrands, "Id", "Name", autoModel.AutoBrandID);
            return View(autoModel);
        }

        // GET: AutoModelsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoModel autoModel = db.AutoModels.Find(id);
            if (autoModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutoBrandID = new SelectList(db.AutoBrands, "Id", "Name", autoModel.AutoBrandID);
            return View(autoModel);
        }

        // POST: AutoModelsAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AutoBrandID")] AutoModel autoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutoBrandID = new SelectList(db.AutoBrands, "Id", "Name", autoModel.AutoBrandID);
            return View(autoModel);
        }

        // GET: AutoModelsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoModel autoModel = db.AutoModels.Find(id);
            if (autoModel == null)
            {
                return HttpNotFound();
            }
            return View(autoModel);
        }

        // POST: AutoModelsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutoModel autoModel = db.AutoModels.Find(id);
            db.AutoModels.Remove(autoModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
