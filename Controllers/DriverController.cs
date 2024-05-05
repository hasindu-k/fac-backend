using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using teaFactory.Business;
using teaFactory.Models;

namespace teaFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private DriverDataContext DriverDataContext;

        public DriverController(IConfiguration config)
        {
            DriverDataContext = new DriverDataContext(config);
        }

        [Route("GetAlldrivers")]
        [HttpGet]

        public async Task<IActionResult> GetAlldrivers()
        {
            try
            {
                var drivers = DriverDataContext.GetAlldrivers();
                return Ok(drivers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [Route("Postdrivers")]
        [HttpPost]

        public async Task<IActionResult> Postdrivers(DriverModel obj)
        {
            bool result = false;
            try
            {
                result = DriverDataContext.Postdrivers(obj);
            }
            catch (Exception ex)
            {
                // log.Error("Error in Postdrivers", ex);
            }

            return Ok(result);
        }

        [Route("Updatedrivers")]
        [HttpPut]

        public async Task<IActionResult> Updatedrivers(DriverModel obj)
        {
            bool result = false;
            try
            {
                result = DriverDataContext.Updatedrivers(obj);
            }
            catch (Exception ex)
            {
                // log.Error("Error in Updatedrivers", ex);
            }

            return Ok(result);
        }

        [Route("Deletedriver")]
        [HttpDelete]
        public async Task<IActionResult> Deletedriver(String driverId)
        {
            bool result = false;
            try
            {
                result = DriverDataContext.Deletedriver(driverId);
            }
            catch (Exception ex)
            {
                //log.Error("Error in Deletedrivers", ex);
            }

            return Ok(result);
        }
    }
}
