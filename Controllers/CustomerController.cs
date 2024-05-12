using Microsoft.AspNetCore.Mvc;
using ITP_PROJECT.Business;
using ITP_PROJECT.Models;

namespace ITP_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private CustomerDataContext customerDataContext;

        public CustomerController(IConfiguration config)
        {
            customerDataContext = new CustomerDataContext(config);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(CustomerLoginRequest request)
        {
            try
            {
                CustomerDataContext customerDataContext = new CustomerDataContext(configuration);
                var isValid = customerDataContext.ValidateCustomer(request);
                if (isValid)
                {
                    // Assuming you have a session mechanism to store the logged user's ID or other necessary information.
                    HttpContext.Session.SetString("LoggedInUserID", request.cusID);
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

        [Route("GetAllCustomers")]
        [HttpGet]

        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = customerDataContext.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }


        [Route("PostCustomers")]
        [HttpPost]

        public async Task<IActionResult> PostCustomers(CustomerModel obj)
        {
            bool result = false;
            try
            {
                result = customerDataContext.PostCustomers(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }

        [Route("UpdateCustomers")]
        [HttpPut]

        public async Task<IActionResult> UpdateCustomers(CustomerModel obj)
        {
            bool result = false;
            try
            {
                result = customerDataContext.UpdateCustomers(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }

        [Route("DeleteCustomer")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int cusID)
        {
            bool result = false;
            try
            {
                result = customerDataContext.DeleteCustomer(cusID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }


        [Route("GetCustomerDetails")]
        [HttpGet]

        public async Task<IActionResult> GetCustomerDetails(int cusID)
        {
            try
            {
                var customers = customerDataContext.GetCustomerDetails(cusID);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }
    }
}

public class CustomerLoginRequest
{
    public string cusID { get; set; }
    public string cusPassword { get; set; }

}

