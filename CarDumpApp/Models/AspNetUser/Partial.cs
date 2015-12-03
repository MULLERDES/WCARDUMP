using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDumpApp.Models
{
    public partial class AspNetUser
    {
        public override string ToString()
        {
            return Email;
        }

    }

    public class AspNetUserDetailsViewModel
    {
        public AspNetUser User { get; set; }
        public List<AspNetRole> Roles { get; set; }

        public string RoleId { get; set; } = "";
        public IEnumerable<SelectListItem> RoleItems
        {
            get { return new SelectList(Roles, "Id", "Name"); }
        }

    }
}