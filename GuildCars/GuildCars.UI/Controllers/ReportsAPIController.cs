using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using GuildCars.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class ReportsAPIController : ApiController
    {
        [Route("api/reports/inventory/new")]
        [AcceptVerbs("GET")]
        public IHttpActionResult InventoryNew()
        {
            var newRepo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var result = newRepo.GetNewInventory();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/reports/inventory/used")]
        [AcceptVerbs("GET")]
        public IHttpActionResult InventoryUsed()
        {
            var usedRepo = VehicleRepositoryFactory.GetRepository();

            try
            {
                var result = usedRepo.GetUsedInventory();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //right now this is using the Test sproc
        [Route("api/reports/sales")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetsSalesReport(string userName, DateTime? minDate, DateTime? maxDate)
        {
            var salesRepo = SalesRepositoryFactory.GetRepository();

            try
            {
                var parameters = new ReportSearchParameters()
                {
                    UserName = userName,
                    MinDate = minDate,
                    MaxDate = maxDate,
                };

                var result = salesRepo.SearchAllSales(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/reports/getusers")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetUsers()
        {
            var userList = AuthorizeUtilities.GetAllUsers();

            try
            {
                return Ok(userList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
