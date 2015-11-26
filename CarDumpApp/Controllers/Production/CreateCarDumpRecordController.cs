using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;
using Microsoft.AspNet.Identity;

namespace CarDumpApp.Controllers.Production
{
    public class CreateCarDumpRecordController :Controller
    {
        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();
        // GET: CreateCarDumpRecord
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult Create()
        //{
        //    ViewBag.AccessLevelID = new SelectList(db.CarDumpAccessLevels, "Id", "Name");
        //    return View();
        //}
        public ActionResult Create(int?  id)
        {
            ViewBag.AccessLevelID = new SelectList(db.CarDumpAccessLevels, "Id", "Name");

            if(id != null)
            {
                CarDump Cd = (from s in db.CarDumps where s.Id == id select s).First();

                StoredFile File1 = (from s in db.StoredFiles where s.ParentCarDumpRecordId == Cd.Id orderby s.Id ascending select s).ToList().Last();
                ViewBag.file1 = File1.Id;
                return View(Cd);
            }
            return View(new CarDump());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarDump _CarDump,string file1, string img1,string img2,string img3)
        {
            // ищем файлы и назначаем владельца
            int automodelid = Request["DDListAutoModel"].ToInt();
            int moduleid = Request["DDListModule"].ToInt();
            int memoryid = Request["DDListMemory"].ToInt();
            _CarDump.AutoModelId = automodelid;
            _CarDump.ModuleID = moduleid;
            _CarDump.MemoryTypeID = memoryid;
            _CarDump.Pic1Url = img1;
            _CarDump.Pic2Url = img2;
            _CarDump.PostedUserID = User.Identity.GetUserId();
            if(_CarDump.Id == 0)
            {
                db.CarDumps.Add(_CarDump);
                db.SaveChanges();
            }
            else
            {
                db.Entry(_CarDump).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            {
                //files process
                if(file1?.ToInt() >= 0)
                {
                    int fileid = file1.ToInt();
                    var f = (from c in db.StoredFiles where c.Id == fileid select c).ToList()[0];
                    f.ParentCarDumpRecordId = _CarDump.Id;
                    db.Entry(f).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
 
            }




            return RedirectToAction("Index");
          
        }



        [HttpPost]
        public String Upload()
        {
            foreach(string file in Request.Files)
            {
                var upload = Request.Files[file];
                if(upload != null)
                {
                    
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);

                    StoredFile sf = new StoredFile();

                    System.IO.Stream s = upload.InputStream;
                    byte[] buff = new byte[s.Length];
                    s.Read(buff, 0, buff.Length);
                    sf.FileData = buff;
                    sf.OriginalFileName = fileName;

                    db.StoredFiles.Add(sf);
                    db.SaveChanges();

                    return sf.Id.ToString();

                   
                }
            }
            return "-1";
        }

        [HttpPost]
        public String UploadPic()
        {
            foreach(string file in Request.Files)
            {
                var upload = Request.Files[file];
                if(upload != null)
                {
                    // получаем имя файла
                    string fileName = (Guid.NewGuid().ToString())+ System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath("~/UploadFilesPics/" + fileName));
                    return fileName;
                }
            }
            return "";
        }
    }
}