
using ITP_PROJECT.Business;
using ITP_PROJECT;
using ITP_PROJECT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITP_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerFeedbackController : ControllerBase
    {
        private ManagerFeedbackDataContext ManagerFeedbackDataContext;


        public ManagerFeedbackController(IConfiguration config)
        {
            ManagerFeedbackDataContext = new ManagerFeedbackDataContext(config);
        }

        [Route("GetAllManagerFeedbacks")]
        [HttpGet]
        public async Task<IActionResult> GetAllManagerFeedbacks()
        {
            try
            {
                var feedbacks = ManagerFeedbackDataContext.GetAllManagerlFeedbacks();
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
               
            }
        }

        [Route("UpdateManagerFeedback")] // Route name changed for clarity
        [HttpPut]
        public async Task<IActionResult> UpdateManagerFeedback(ManagerFeedbackModel obj)
        {
            bool result = false;
            try
            {
                // Assuming FeedbackDataContext has a method for updating replyFeedback
                result = ManagerFeedbackDataContext.UpdateManagerFeedback(obj);
                if (result)
                {
                    return Ok("Reply updated successfully.");
                }
                else
                {
                    return NotFound("Reply with the specified reply ID was not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Consider more informative error message for debugging
                return StatusCode(500, "An error occurred while updating feedback.");
            }

            return Ok(result);
        }

        


    }
}
