using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibrary_Database.Models.Tables;
using DvdLibrary_Database.Data.Interfaces;
using DvdLibrary_Database.Models.Queries;

namespace DvdLibrary_Database.Data.Mock
{
    public class DvdRepositoryMock : IDvdRepository
    {
        private static List<DvdView> _dvds;
        
        static DvdRepositoryMock()
        {
           
            _dvds = new List<DvdView>()
            {
                new DvdView { DvdId=1, Title="High Art", Notes="Indie film.", ReleaseYearName=1998 , DirectorName="Lisa Cholodenko", RatingType="R" },
                new DvdView { DvdId=2, Title="Wonder Woman", Notes="Super hero film.", ReleaseYearName=2017, DirectorName="Patty Jenkins", RatingType="PG-13" },
                new DvdView { DvdId=3, Title="Lost In Translation", Notes=null, ReleaseYearName=2003, DirectorName="Sofia Coppola", RatingType="R"},
                new DvdView { DvdId=4, Title="Bend It Like Beckham", Notes = null, ReleaseYearName=2002, DirectorName="Gurinder Chadha", RatingType="PG-13"},
                new DvdView {DvdId=5, Title="The Hurt Locker", Notes=null, ReleaseYearName=2008, DirectorName="Kathryn Bigelow", RatingType="R"}
            };

        }

        public IEnumerable<DvdView> GetAll()
        {
            return _dvds;
        }

        public DvdView GetById(int dvdId)
        {
            return _dvds.FirstOrDefault(c => c.DvdId == dvdId);
        }

        public IEnumerable<DvdView> GetByTitle(string title)
        {
            return _dvds.Where(d => d.Title == title);
        }
        public IEnumerable<DvdView> GetByReleaseYear(int realeaseYear)
        {
            return _dvds.Where(d => d.ReleaseYearName == realeaseYear);
        }

        public IEnumerable<DvdView> GetByDirector(string director)
        {
            return _dvds.Where(d => d.DirectorName == director);
        }

        public IEnumerable<DvdView> GetByRating(string rating)
        {
            return _dvds.Where(d => d.RatingType == rating);
        }

        public void Create(DvdView newDvd)
        {
            if (_dvds.Any())
            {
                newDvd.DvdId = _dvds.Max(c => c.DvdId) + 1;
            }
            else
            {
                newDvd.DvdId = 0;
            }

            _dvds.Add(newDvd);
        }

        public void Update(int id, DvdView dvd)
        {
            _dvds.RemoveAll(c => c.DvdId == id);

            dvd.DvdId = id;
            

            _dvds.Add(dvd);
        }

        public void Delete(int dvdId)
        {
            _dvds.RemoveAll(c => c.DvdId == dvdId);
        }
    }
}

