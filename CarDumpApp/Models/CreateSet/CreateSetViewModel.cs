using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDumpApp.Models;

namespace CarDumpApp.Models
{
    public class CreateSetViewModel
    {
        public CarDumpSet CDSet { get; set; }
        public List<CarDump> Cardumps { get; set; }
        //public AutoModel AutoModel { get; set; }
       
    }
}