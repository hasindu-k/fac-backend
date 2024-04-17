using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ITP_PROJECT.Business;
using ITP_PROJECT.Models.;

namespace ITP_PROJECT.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public UserController(IConfiguration config)
        {
            configuration = config;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            try
            {
                UserDataContext userDataContext = new UserDataContext(configuration);
                var result = userDataContext.ValidateUser(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }

   
}
