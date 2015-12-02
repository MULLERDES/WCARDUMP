using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CarDumpApp.Models
{
    public class SearchSetsViewModel
    {

        public static int PerPageNumber
        {
            get; set;
        } = 25;
        public List<CarDumpSet> Sets { get; set; }
        public int CurrentPage { get; set; } = 0;
        public int MaximunPages { get; set; }

        public string SearchParameters { get; set; }
    }
}