using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary_Database.Models.Tables
{
    public class Dvd
    {
        public int DvdId { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public int ReleaseYearId { get; set; }
        public int DirectorId { get; set; }
        public int RatingId { get; set; }
    }
}
