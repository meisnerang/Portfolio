using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class SalesReportViewModel
    {
        public IEnumerable<SelectListItem> User { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public Sales Sale { get; set; }
    }
}