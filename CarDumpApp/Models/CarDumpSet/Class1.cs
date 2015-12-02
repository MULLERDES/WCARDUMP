using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDumpApp.Models
{
    public partial  class CarDumpSet
    {
        public string PersonalNumber
        {
            get
            {
                return Id.ToString("D6");
            }
        }
    }

    public class CardumpSetDetailsViewModel
    {
        public CarDumpSet Set { get; set; }
        public List<CarDump> CarDumps { get; set; }

    }


}