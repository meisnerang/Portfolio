using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Sales
    {
        public int SalesId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public int ZipCode { get; set; }
        public int PurchaseTypeId { get; set; }
        public int VehicleId { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
