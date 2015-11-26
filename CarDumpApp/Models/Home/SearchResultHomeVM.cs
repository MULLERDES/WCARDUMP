using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDumpApp.Models
{
    public class SearchResultHomeVM
    {
        private int perpagecount = 10;
        public List<CarDump> Cardumps { get; set; }
        public int CurrentPage { get; set; } = 0;
        public int MaximunPages { get; set; }

        public string SearchParameters { get; set; }
    }
}