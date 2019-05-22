using GuildCars.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class HomeAPIController : ApiController
    {
        [Route("api/home/featured")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetFeatured()
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetFeatured();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/home/specials")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSpecials()
        {
            var repo = SpecialRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
