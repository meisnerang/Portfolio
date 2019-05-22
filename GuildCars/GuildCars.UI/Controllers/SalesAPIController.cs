using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class SalesAPIController : ApiController
    {
        [Route("api/sales/index/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(string makeModelOrYear, decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var parameters = new VehicleSearchParameters()
                {
                    MakeModelOrYear = makeModelOrYear,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear
                };

                var result = repo.SearchAllNotSoldInventory(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/sales/index/search/id")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchById(int id)
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            var result = repo.GetById(id);

            return Ok(result);
        }
    }
}
