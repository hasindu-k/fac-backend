using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Bussiness;
using Product.Models;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private static readonly ILog log = LogManager.GetLogger(typeof(CartController));
        public UserController(IConfiguration config)
        {
            configuration = config;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(ManagerModel request)
        {
            try
            {
                UserDataContext userDataContext = new UserDataContext(configuration);
                var result = userDataContext.ValidateUser(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Error in login", ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
