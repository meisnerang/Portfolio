using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class FeaturedItem
    {
        public int VehicleId { get; set; }
        public string PhotoFile { get; set; }
        public int Year { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public decimal SalePrice { get; set; }
        public Special Special { get; set; }
    }
}
