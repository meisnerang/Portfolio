using DvdLibrary_Database.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibrary_Database.Models.Queries;
using DvdLibrary_Database.Models;
using DvdLibrary_Database.Data.EF;

namespace DvdLibrary_Database.Data.EF
{
    public class DvdRepositoryEF : IDvdRepository
    {
        public void Create(DvdView newDvd)
        {
            var repository = new DvdLibraryEntities();
            var repoDvd = new Dvd();
            ReleaseYear year = ReleaseYearRepositoryEF.GetReleaseYearIdFromName(newDvd.ReleaseYearName);
            Director director = DirectorRepositoryEF.GetDirectorIdFromName(newDvd.DirectorName);
            Rating rating = RatingRepositoryEF.GetRatingIdFromName(newDvd.RatingType);

            repoDvd.DvdId = newDvd.DvdId;
            repoDvd.Title = newDvd.Title;
            repoDvd.Notes = newDvd.Notes;
            repoDvd.ReleaseYearId = year.ReleaseYearId;
            repoDvd.DirectorId = director.DirectorId;
            repoDvd.RatingId = rating.RatingId;

            repository.Dvds.Add(repoDvd);
            repository.SaveChanges();
        }

        public void Delete(int dvdId)
        {
            var repository = new DvdLibraryEntities();
            var dvd = repository.Dvds.FirstOrDefault(d => d.DvdId == dvdId);

            if(dvd != null)
            {
                repository.Dvds.Remove(dvd);
                repository.SaveChanges();
            }
        }

        public IEnumerable<DvdView> GetAll()
        {
            List<DvdView> dvds = new List<DvdView>();
            var repository = new DvdLibraryEntities();
            
            var model = from dvd in repository.Dvds
                    select new DvdView
                    {
                     DvdId = dvd.DvdId,
                     Title = dvd.Title,
                     Notes = dvd.Notes,
                     ReleaseYearName = dvd.ReleaseYear.ReleaseYearName,
                     DirectorName = dvd.Director.DirectorName,
                     RatingType = dvd.Rating.RatingType
                     };
            
            return model.ToList();
        }

        public IEnumerable<DvdView> GetByDirector(string director)
        {
            List<DvdView> dvds = new List<DvdView>();
            var repository = new DvdLibraryEntities();
            var model = from dvd in repository.Dvds
                        select new DvdView
                        
                        {
                            DvdId = dvd.DvdId,
                            Title = dvd.Title,
                            Notes = dvd.Notes,
                            ReleaseYearName = dvd.ReleaseYear.ReleaseYearName,
                            DirectorName = dvd.Director.DirectorName,
                            RatingType = dvd.Rating.RatingType
                        };
            foreach (var dvd in model)
            {
                if(dvd.DirectorName.Contains(director))
                {
                    dvds.Add(dvd);
                }
             }
            return dvds;
        }

        public IEnumerable<DvdView> GetByRating(string rating)
        {
            List<DvdView> dvds = new List<DvdView>();
            var repository = new DvdLibraryEntities();
            var model = from dvd in repository.Dvds
                        select new DvdView
                        {
                            DvdId = dvd.DvdId,
                            Title = dvd.Title,
                            Notes = dvd.Notes,
                            ReleaseYearName = dvd.ReleaseYear.ReleaseYearName,
                            DirectorName = dvd.Director.DirectorName,
                            RatingType = dvd.Rating.RatingType
                        };
            foreach (var dvd in model)
            {
                if (dvd.RatingType.Contains(rating))
                {
                    dvds.Add(dvd);
                }
            }
            return dvds;
        }

        public DvdView GetById(int dvdId)
        {
            DvdView dvds = new DvdView();
            var repository = new DvdLibraryEntities();
            var model = from dvd in repository.Dvds
                        select new DvdView
                        {
                            DvdId = dvd.DvdId,
                            Title = dvd.Title,
                            Notes = dvd.Notes,
                            ReleaseYearName = dvd.ReleaseYear.ReleaseYearName,
                            DirectorName = dvd.Director.DirectorName,
                            RatingType = dvd.Rating.RatingType
                        };
            foreach (var dvd in model)
            {
                if (dvd.DvdId.Equals(dvdId))
                {
                    dvds.DvdId = dvdId;
                    dvds.Title = dvd.Title;
                    dvds.Notes = dvd.Notes;
                    dvds.ReleaseYearName = dvd.ReleaseYearName;
                    dvds.DirectorName = dvd.DirectorName;
                    dvds.RatingType = dvd.RatingType;
                }
            }
            return dvds;
        }

        public IEnumerable<DvdView> GetByReleaseYear(int releaseYear)
        {
            List<DvdView> dvds = new List<DvdView>();
            var repository = new DvdLibraryEntities();
            var model = from dvd in repository.Dvds
                        select new DvdView
                        {
                            DvdId = dvd.DvdId,
                            Title = dvd.Title,
                            Notes = dvd.Notes,
                            ReleaseYearName = dvd.ReleaseYear.ReleaseYearName,
                            DirectorName = dvd.Director.DirectorName,
                            RatingType = dvd.Rating.RatingType
                        };
            foreach (var dvd in model)
            {
                if (dvd.ReleaseYearName.Equals(releaseYear))
                {
                    dvds.Add(dvd);
                }
            }
            return dvds;
        }

        public IEnumerable<DvdView> GetByTitle(string title)
        {
            List<DvdView> dvds = new List<DvdView>();
            var repository = new DvdLibraryEntities();
            var model = from dvd in repository.Dvds
                        select new DvdView
                        {
                            DvdId = dvd.DvdId,
                            Title = dvd.Title,
                            Notes = dvd.Notes,
                            ReleaseYearName = dvd.ReleaseYear.ReleaseYearName,
                            DirectorName = dvd.Director.DirectorName,
                            RatingType = dvd.Rating.RatingType
                        };
            foreach (var dvd in model)
            {
                if (dvd.Title.Contains(title))
                {
                    dvds.Add(dvd);
                }
            }
            return dvds;
        }

        public void Update(int id, DvdView dvd)
        {
            var repository = new DvdLibraryEntities();
            var repoDvd = new Dvd();
            ReleaseYear year = ReleaseYearRepositoryEF.GetReleaseYearIdFromName(dvd.ReleaseYearName);
            Director director = DirectorRepositoryEF.GetDirectorIdFromName(dvd.DirectorName);
            Rating rating = RatingRepositoryEF.GetRatingIdFromName(dvd.RatingType);
            //if I delete the Delete() then I end up with the original dvd & a dew one
            Delete(id);

            repoDvd.DvdId = id;
            repoDvd.Title = dvd.Title;
            repoDvd.Notes = dvd.Notes;
            repoDvd.ReleaseYearId = year.ReleaseYearId;
            repoDvd.DirectorId = director.DirectorId;
            repoDvd.RatingId = rating.RatingId;

            repository.Dvds.Add(repoDvd);
            repository.SaveChanges();
        }
    }
}
