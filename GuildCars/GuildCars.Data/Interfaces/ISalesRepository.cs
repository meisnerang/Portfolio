using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ISalesRepository
    {
        IEnumerable<VehicleItem> GetAll();
        void Add(SaleItem sale);
        IEnumerable<SalesReportItem> SearchAllSales(ReportSearchParameters parameters);
    }
}
