using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ITP_PROJECT.Business;
using ITP_PROJECT.Models;

namespace ITP_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private UserDataContext userDataContext;

        public UserController(IConfiguration config)
        {
            userDataContext = new UserDataContext(config);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            try
            {
                UserDataContext userDataContext = new UserDataContext(configuration);
                var isValid = userDataContext.ValidateUser(request);
                if (isValid)
                {
                    // Assuming you have a session mechanism to store the logged user's ID or other necessary information.
                    HttpContext.Session.SetString("LoggedInUserID", request.managerID);
                    return Ok("Login successful");
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }



        [Route("GetAllManagers")]
        [HttpGet]

        public async Task<IActionResult> GetAllManagers()
        {
            try
            {
                var managers = userDataContext.GetAllManagers();
                return Ok(managers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        [Route("PostManagers")]
        [HttpPost]

        public async Task<IActionResult> PostManagers(UserModel obj)
        {
            bool result = false;
            try
            {
                result = userDataContext.PostManagers(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }

        [Route("UpdateManagers")]
        [HttpPut]

        public async Task<IActionResult> UpdateManagers(UserModel obj)
        {
            bool result = false;
            try
            {
                result = userDataContext.UpdateManagers(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }

        [Route("DeleteManager")]
        [HttpDelete]
        public async Task<IActionResult> DeleteManager(string managerID)
        {
            bool result = false;
            try
            {
                result = userDataContext.DeleteUser(managerID);
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

public class UserLoginRequest
{
    public string managerID { get; set; }
    public string managerPassword { get; set; }
}
