using DvdLibrary_Database.Data.Factories;
using DvdLibrary_Database.Models.Queries;
using DvdLibrary_Database.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdLibrary_Database.UI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        [Route("dvds/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllDvds()
        {
            var repo = DvdRepositoryFactory.GetRepository();

            return Ok(repo.GetAll());
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetById(int id)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            DvdView found = repo.GetById(id);

            if (found == null)
            {
                //returns a 404 HTTP response
                return NotFound();
            }

            return Ok(found);
        }

        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByTitle(string title)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            IEnumerable<DvdView> found = repo.GetByTitle(title);

            if (found == null)
            {
                //returns a 404 HTTP response
                return NotFound();
            }

            return Ok(found);
        }

        [Route("dvds/year/{releaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByYear(int releaseYear)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            IEnumerable<DvdView> found = repo.GetByReleaseYear(releaseYear);

            if (found == null)
            {
                //returns a 404 HTTP response
                return NotFound();
            }

            return Ok(found);
        }

        [Route("dvds/director/{directorName}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByDirector(string directorName)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            IEnumerable<DvdView> found = repo.GetByDirector(directorName);

            if (found == null)
            {
                //returns a 404 HTTP response
                return NotFound();
            }

            return Ok(found);
        }

        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByRating(string rating)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            IEnumerable<DvdView> found = repo.GetByRating(rating);

            if (found == null)
            {
                //returns a 404 HTTP response
                return NotFound();
            }

            return Ok(found);
        }

        [Route("dvd")]
        [AcceptVerbs("Post")]
        public IHttpActionResult Add(DvdView dvd)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            repo.Create(dvd);

            //Created() returns a 201 HTTP response
            //lets client know the object was created & it has an id
            return Created($"dvd/{dvd.DirectorName}", dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public void Update(int id, DvdView dvd)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            repo.Update(id, dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            repo.Delete(id);
        }
    }
}
