using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class VehicleItem
    {
        public int VehicleId { get; set; }
        public string PhotoFile { get; set; }
        public int Year { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string BodyStyleName { get; set; }
        public bool IsManualTransmission { get; set; }
        public string ExteriorColor { get; set; }
        public string InteriorColor { get; set; }
        public int Mileage { get; set; }
        public string Vin { get; set; }
        public string Description { get; set; }
        public decimal SalePrice { get; set; }
        public decimal MSRP { get; set; }
        public bool IsSold { get; set; }
    }
}
