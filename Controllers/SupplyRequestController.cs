using ITP_PROJECT.Business;
using ITP_PROJECT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITP_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyRequestController : ControllerBase
    {
        private readonly SupplyRequestDataContext supplyRequestDataContext;

        public SupplyRequestController(IConfiguration config)
        {
            supplyRequestDataContext = new SupplyRequestDataContext(config);
        }

        [Route("GetAllSupplyRequests")]
        [HttpGet]
        public async Task<IActionResult> GetAllSupplyRequests()
        {
            try
            {
                var supplyRequests = supplyRequestDataContext.GetAllSupplyRequests();
                return Ok(supplyRequests);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Route("CreateSupplyRequest")]
        [HttpPost]
        public async Task<IActionResult> CreateSupplyRequest(SupplyRequestModel request)
        {
            try
            {
                bool result = supplyRequestDataContext.CreateSupplyRequest(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Route("UpdateSupplyRequest")]
        [HttpPut]
        public async Task<IActionResult> UpdateSupplyRequest(SupplyRequestModel request)
        {
            try
            {
                bool result = supplyRequestDataContext.UpdateSupplyRequest(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Route("DeleteSupplyRequest/{requestId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSupplyRequest(int requestId)
        {
            try
            {
                bool result = supplyRequestDataContext.DeleteSupplyRequest(requestId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }

}
