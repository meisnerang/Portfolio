using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class AdminAPIController : ApiController
    {
        [Route("api/admin/search")]
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

        [Route("api/admin/makes")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetMakes()
        {
            var repo = MakeRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetAllMakeItem();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/admin/models")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModels()
        {
            var repo = ModelRepositoryFactory.GetRepository();

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

        
        [Route("api/admin/modelsformake")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModelsForMake(int makeId)
        {
            var repo = ModelRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetModelsByMake(makeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/admin/specials")]
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

        [Route("api/admin/delete/{specialId}")]
        [AcceptVerbs("Delete")]
        public IHttpActionResult DeleteSpecial(int specialId)
        {
            var repo = SpecialRepositoryFactory.GetRepository();

            try
            {
                repo.Delete(specialId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
