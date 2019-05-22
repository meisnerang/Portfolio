using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ISpecialRepository
    {
        IEnumerable<Special> GetAll();
        Special GetById(int specialId);
        void Delete(int specialId);
        void Add(Special special);
    }
}
