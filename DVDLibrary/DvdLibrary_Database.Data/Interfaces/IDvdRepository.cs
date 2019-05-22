using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibrary_Database.Models.Tables;
using DvdLibrary_Database.Models.Queries;

namespace DvdLibrary_Database.Data.Interfaces
{
    public interface IDvdRepository
    {
        IEnumerable<DvdView> GetAll();
        DvdView GetById(int dvdId);
        IEnumerable<DvdView> GetByTitle(string title);
        IEnumerable<DvdView> GetByReleaseYear(int releaseYear);
        IEnumerable<DvdView> GetByDirector(string director);
        IEnumerable<DvdView> GetByRating(string rating);
        void Create(DvdView newDvd);
        void Update(int id, DvdView dvd);
        void Delete(int dvdId);

    }
}
