using DvdLibrary_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary_Database.Data.EF
{
    public class DirectorRepositoryEF
    {
        public static Director GetDirectorIdFromName(string directorName)
        {
            var repository = new DvdLibraryEntities();
            var directorId = repository.Directors.FirstOrDefault(d => d.DirectorName == directorName);

            return directorId;
        }
    }
}
