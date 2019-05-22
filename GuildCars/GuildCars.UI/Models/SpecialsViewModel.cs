using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class SpecialsViewModel
    {
        public Special Special { get; set; }
        public IEnumerable<Special> SpecialList { get; set; }
    }
}