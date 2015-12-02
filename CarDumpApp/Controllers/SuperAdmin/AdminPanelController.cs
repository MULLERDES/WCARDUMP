using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;

namespace CarDumpApp.Controllers.SuperAdmin
{

    [Authorize]
    public class AdminPanelController :Controller
    {
        CarDumpDatabaseEntities db = new CarDumpDatabaseEntities();
        // GET: AdminPanel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FullMenu()
        {
            return PartialView();
        }


        public ActionResult CarDumpsListByUserId(string userId, int? accessId, int? modelID, int? year, bool? chkSate)
        {
            IEnumerable<CarDump> CD = null;// (from s in db.CarDumps where s.PostedUserID == userId select s).ToList();
            //if(accessId == null)
            //{
            //    CD = (from s in db.CarDumps where s.PostedUserID == userId select s).ToList();
            //}
            //else
            //{
            //    CD = (from s in db.CarDumps where (s.PostedUserID == userId) && 
            //          (accessId==null?true:s.AccessLevelID == accessId)&& 
            //          (modelID==null||year==null)?true:           (s.Year==year&&s.AutoModelId==modelID)
            //          select s).ToList();
            //}

            CD = db.CarDumps.Where(
                s =>
                        (chkSate == null ? true : s.Checked == chkSate) &&
                        (userId == null ? true : s.PostedUserID == userId) &&
                        (accessId == null ? true : s.AccessLevelID == accessId) &&
                        ((year == null) ? true : s.Year == year) &&
                        ((modelID == null) ? true : s.AutoModelId == modelID)
                ).OrderBy(s => s.Id).Skip(0).ToArray();
            return PartialView(CD);
        }

        public ActionResult CheckUnchecked(string sid)
        {
            IEnumerable<CarDump> CD = null;// (from s in db.CarDumps where s.PostedUserID == userId select s).ToList();
            
            if(sid != null)
            {
                int id = Convert.ToInt32(sid);
                try
                {
                    CarDump cd = db.CarDumps.Where(s => s.Id == id).First();
                    cd.Checked = true;
        
                    db.Entry(cd).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();
                    db = new CarDumpDatabaseEntities();
                }
                catch(Exception)
                {

                }
            }



            CD = db.CarDumps.Where(
                s =>( s.Checked == false )&&(s.AccessLevelID==1)
                ).OrderBy(s => s.Id).Skip(0).ToArray();
            List<AspNetUser> Users;
            using(var udb= new UsersMDLConnection())
            {
                Users = udb.AspNetUsers.ToList();
                foreach(var item in CD)
                {
                    item.PostedUser = Users.Find(tf => tf.Id == item.PostedUserID);
                }
            }

            return View(CD);
        }

        [HttpGet]
        public int NumbersOfCardumps(string postedUserId)
        {
            return db.CarDumps.Count(c => ( postedUserId==null?true: c.PostedUserID == postedUserId));
        }
        //public ActionResult CardumpSetsByUserId
    }
}