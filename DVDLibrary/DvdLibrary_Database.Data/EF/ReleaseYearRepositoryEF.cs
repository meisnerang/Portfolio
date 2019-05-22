using DvdLibrary_Database.Models;
using DvdLibrary_Database.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary_Database.Data.EF
{
    public class ReleaseYearRepositoryEF
    {
        public static ReleaseYear GetReleaseYearIdFromName(int yearName)
        {
            var repository = new DvdLibraryEntities();
            var yearId = repository.ReleaseYears.FirstOrDefault(r => r.ReleaseYearName == yearName);

            return yearId;
         }
    }
}
