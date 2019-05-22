using DvdLibrary_Database.Models;
using DvdLibrary_Database.Models.Queries;
using DvdLibrary_Database.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary_Database.Data.EF
{
    public class RatingRepositoryEF
    {
        public static Models.Rating GetRatingIdFromName(string ratingType)
        {
            var repository = new DvdLibraryEntities();
            var directorId = repository.Ratings.FirstOrDefault(r => r.RatingType == ratingType);

            return directorId;
        }
    }
}
