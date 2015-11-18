using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDumpApp.Models;

namespace CarDumpApp.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            //CarDumpSearchViewModel vm = new CarDumpSearchViewModel()
            //{
            //    AutoBrands = new SelectList(brands, "Id", "Name", selectedBrand),
            //    Models = new SelectList(models, "Id", "Name", selectedModel),
            //    CarDumps = cd
            //};
            return View();
        }
    }
}
