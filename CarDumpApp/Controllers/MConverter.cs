using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDumpApp.Controllers
{
    public static class MConverter
    {
        public static int ToInt(this string val)
        {
            int ret = -1;
            int.TryParse(val, out ret);
            return ret;
        }
    }
}