using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarDumpApp.Controllers.SuperAdmin
{
    public class AspNetUsersController :Controller
    {
        private UsersDBEntities db = new UsersDBEntities();

        // GET: AspNetUsers
        public async Task<ActionResult> Index()
        {
            return View(await db.AspNetUsers.ToListAsync());
        }

        // GET: AspNetUsers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            AspNetUserDetailsViewModel mdl = new AspNetUserDetailsViewModel();
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mdl.User = await db.AspNetUsers.FindAsync(id);
            if(mdl.User == null)
            {
                return HttpNotFound();
            }
            mdl.Roles = await db.AspNetRoles.ToListAsync();

            var ctx = new ApplicationDbContext();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

            if(userManager.IsInRole(mdl.User.Id, "NetUser"))
            {
                mdl.RoleId = RoleManager.FindByName("NetUser")?.Id;
            }
            if(userManager.IsInRole(mdl.User.Id, "Admin"))
            {
                mdl.RoleId = RoleManager.FindByName("Admin")?.Id;
            }


            return View(mdl);
        }

        [HttpPost]
        public void Details(AspNetUserDetailsViewModel mdl)
        {

            var ctx = new ApplicationDbContext();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));


            //if(userManager.IsInRole(mdl.User.Id, "Admin"))
            //{
            //    mdl.RoleId = RoleManager.FindByName("Admin")?.Id;
            //}
            //if(userManager.IsInRole(mdl.User.Id, "NetUser"))
            //{
            //    mdl.RoleId = RoleManager.FindByName("NetUser")?.Id;
            //}


            var role = RoleManager.FindById(mdl.RoleId);
            var roles = RoleManager.Roles.ToList();
            foreach(var item in roles)
            {
                userManager.RemoveFromRole(mdl.User.Id, item.Name);
            }
            userManager.AddToRole(mdl.User.Id, role.Name);
        }

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetUsers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: AspNetUsers/Edit/5


        // POST: AspNetUsers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: AspNetUsers/Delete/5


        // POST: AspNetUsers/Delete/5


        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
