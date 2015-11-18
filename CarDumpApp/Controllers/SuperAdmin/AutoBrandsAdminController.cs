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
    public class AutoBrandsAdminController : Controller
    {
        private CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();

        // GET: AutoBrandsAdmin
        public ActionResult Index()
        {
            return View(db.AutoBrands.ToList());
        }

        // GET: AutoBrandsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoBrand autoBrand = db.AutoBrands.Find(id);
            if (autoBrand == null)
            {
                return HttpNotFound();
            }
            return View(autoBrand);
        }

        // GET: AutoBrandsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutoBrandsAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AutoBrand autoBrand)
        {
            if (ModelState.IsValid)
            {
                db.AutoBrands.Add(autoBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autoBrand);
        }

        // GET: AutoBrandsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoBrand autoBrand = db.AutoBrands.Find(id);
            if (autoBrand == null)
            {
                return HttpNotFound();
            }
            return View(autoBrand);
        }

        // POST: AutoBrandsAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AutoBrand autoBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autoBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autoBrand);
        }

        // GET: AutoBrandsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoBrand autoBrand = db.AutoBrands.Find(id);
            if (autoBrand == null)
            {
                return HttpNotFound();
            }
            return View(autoBrand);
        }

        // POST: AutoBrandsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutoBrand autoBrand = db.AutoBrands.Find(id);
            db.AutoBrands.Remove(autoBrand);
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
