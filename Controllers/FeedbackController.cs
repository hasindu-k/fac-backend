
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Linq.Expressions;
using CustomAffair.Business;
using CustomAffair.Models;

namespace CustomAffair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private FeedbackDataContext FeedbackDataContext;
       

        public FeedbackController(IConfiguration config)
        {
            FeedbackDataContext = new FeedbackDataContext(config);
        }

        [Route("GetAllFeedbacks")]
        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            try
            {
                var feedbacks = FeedbackDataContext.GetAllFeedbacks();
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
                
            }
        }


        [Route("PostFeedbacks")]
        [HttpPost]
        public async Task<IActionResult> PostFeedbacks(FeedbackModel obj)
        {
            bool result = false;
            try
            {
                result = FeedbackDataContext.PostFeedbacks(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }

        [Route("UpdateFeedbacks")]
        [HttpPut]
        public async Task<IActionResult> UpdateFeedbacks(FeedbackModel obj)
        {
            bool result = false;
            try
            {
                result = FeedbackDataContext.UpdateFeedbacks(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return StatusCode(500, "Internal Server Error");
                return StatusCode(500, ex);
            }
            return Ok(result);
        }

        [Route("DeleteFeedback/{fBackID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFeedback(int fBackID)
        {
            
            try
            {
                bool result = FeedbackDataContext.DeleteFeedback(fBackID);
                if (result)
                {
                    return Ok("Leave deleted successfully.");
                }
                else
                {
                    return NotFound("Leave with the specified employee ID was not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

