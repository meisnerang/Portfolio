using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary_Database.Models.Queries
{
    public class DvdView
    {
        public int DvdId { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public int ReleaseYearName { get; set; }
        public string DirectorName { get; set; }
        public string RatingType { get; set; }
    }
}
