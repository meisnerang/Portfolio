using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public int? Year { get; set; }
        public int BodyStyleId { get; set; }

        public bool IsManualTransmission { get; set; }
        public int ExteriorColorId { get; set; }
        public int InteriorColorId { get; set; }
        public string UserId { get; set; }
        public int? Mileage { get; set; }

        public decimal? SalePrice { get; set; }
        public decimal? MSRP { get; set; }
        public string Vin { get; set; }
        public string Description { get; set; }
        public string PhotoFile { get; set; }

        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public bool IsSold { get; set; }
    }
}
