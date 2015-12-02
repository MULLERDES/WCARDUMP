using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDumpApp.Models
{
    public partial class AspNetUser
    {
        public override string ToString()
        {
            return Email;
        }
    }
}