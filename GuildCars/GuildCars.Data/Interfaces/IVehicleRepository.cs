using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IVehicleRepository
    {
        IEnumerable<VehicleItem> GetAll();
        VehicleItem GetById(int vehicleId);
        Vehicle GetByIdByVehicle(int vehicleId);
        int CountOfVehicles();
        int GetMaxVehicleId();
        void Delete(int vehicleId);
        void DeleteSaleVehicle(int vehicleId);
        void Add(Vehicle vehicle);
        void Edit(Vehicle vehicle);
        IEnumerable<FeaturedItem> GetFeatured();
        IEnumerable<VehicleItem> SearchNewInventory(VehicleSearchParameters parameters);
        IEnumerable<VehicleItem> SearchUsedInventory(VehicleSearchParameters parameters);
        IEnumerable<VehicleItem> SearchAllNotSoldInventory(VehicleSearchParameters parameters);
        IEnumerable<InventoryReportItem> GetNewInventory();
        IEnumerable<InventoryReportItem> GetUsedInventory();
    }
}
