using ITP_PROJECT.Business;
using ITP_PROJECT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITP_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FertilizerController : ControllerBase
    {
        private FertilizerDataContext fertilizerDataContext;
       

        public FertilizerController(IConfiguration config)
        {
            fertilizerDataContext = new FertilizerDataContext(config);
        }
        [Route("GetAllFertilizers")]
        [HttpGet]

        public async Task<IActionResult> GetAllFertilizers()
        {
            try
            {
                var fertilizers = fertilizerDataContext.GetAllFertilizers();
                return Ok(fertilizers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

        }

        
        [Route("PostFertilizer")]
        [HttpPost]

        public async Task<IActionResult> PostFertilizer(FertilizerModel obj)
        {
            bool result = false;
            try
            {
                result = fertilizerDataContext.PostFertilizer(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }

        [Route("UpdateFertilizer")]
        [HttpPut]
        public async Task<IActionResult> UpdateFertilizer(FertilizerModel obj)
        {
            bool result = false;
            try
            {
                result = fertilizerDataContext.UpdateFertilizer(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }

        [Route("DeleteFertilizer/{fertilizerId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFertilizer(int fertilizerId)
        {
            bool result = false;
            try
            {
                result = fertilizerDataContext.DeleteFertilizer(fertilizerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }

    }
}
