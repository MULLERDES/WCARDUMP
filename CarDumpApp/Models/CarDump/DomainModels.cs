using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDumpApp.Models
{
    public class CarDumpSearchViewModel
    {
        public SelectList AutoBrands { get; set; }
        public SelectList Models { get; set; }

        public IEnumerable<CarDump> CarDumps { get; set; }
    }

    public class CarDumpCreateModel
    {
        public IEnumerable<AutoBrand> AutoBrands { get; set; }
    }



    
}