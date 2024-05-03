
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Linq.Expressions;
using ITP_PROJECT.Business;
using ITP_PROJECT.Models;

namespace ITP_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackCustomerController : ControllerBase
    {
        private FeedbackCustomerDataContext FeedbackCustomerDataContext;
       

        public FeedbackCustomerController(IConfiguration config)
        {
            FeedbackCustomerDataContext = new FeedbackCustomerDataContext(config);
        }

        [Route("GetCustomerFeedbacks/{customerId}")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerFeedbacks(int customerId)
        {
            try
            {
                var feedbacks = FeedbackCustomerDataContext.GetCustomerFeedbacks(customerId);
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }



        [Route("PostCusFeedbacks")]
        [HttpPost]
        public async Task<IActionResult> PostCusFeedbacks(FeedbackModel obj)
        {
            bool result = false;
            try
            {
                result = FeedbackCustomerDataContext.PostCusFeedbacks(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }

        [Route("UpdateCusFeedbacks")]
        [HttpPut]
        public async Task<IActionResult> UpdateFeedbacks(FeedbackModel obj)
        {
            bool result = false;
            try
            {
                result = FeedbackCustomerDataContext.UpdateCusFeedbacks(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return StatusCode(500, "Internal Server Error");
                return StatusCode(500, ex);
            }
            return Ok(result);
        }

        [Route("DeleteCusFeedback/{fBackID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCusFeedback(int fBackID)
        {
            
            try
            {
                bool result = FeedbackCustomerDataContext.DeleteCusFeedback(fBackID);
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

