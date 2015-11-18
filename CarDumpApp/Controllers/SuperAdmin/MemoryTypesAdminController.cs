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
    public class MemoryTypesAdminController : Controller
    {
        private CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();

        // GET: MemoryTypesAdmin
        public ActionResult Index()
        {
            return View(db.MemoryTypes.ToList());
        }

        // GET: MemoryTypesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemoryType memoryType = db.MemoryTypes.Find(id);
            if (memoryType == null)
            {
                return HttpNotFound();
            }
            return View(memoryType);
        }

        // GET: MemoryTypesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemoryTypesAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] MemoryType memoryType)
        {
            if (ModelState.IsValid)
            {
                db.MemoryTypes.Add(memoryType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memoryType);
        }

        // GET: MemoryTypesAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemoryType memoryType = db.MemoryTypes.Find(id);
            if (memoryType == null)
            {
                return HttpNotFound();
            }
            return View(memoryType);
        }

        // POST: MemoryTypesAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] MemoryType memoryType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memoryType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memoryType);
        }

        // GET: MemoryTypesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemoryType memoryType = db.MemoryTypes.Find(id);
            if (memoryType == null)
            {
                return HttpNotFound();
            }
            return View(memoryType);
        }

        // POST: MemoryTypesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemoryType memoryType = db.MemoryTypes.Find(id);
            db.MemoryTypes.Remove(memoryType);
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
