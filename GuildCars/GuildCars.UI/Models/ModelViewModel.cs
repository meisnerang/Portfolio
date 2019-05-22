using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class ModelViewModel
    {
        public IEnumerable<SelectListItem> Makes { get; set; }
        
        public Model Model { get; set; }
    }
}