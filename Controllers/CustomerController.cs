using Microsoft.AspNetCore.Mvc;
using ITP_PROJECT.Business;
using ITP_PROJECT.Models;

namespace ITP_PROJECT.Controllers
{
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private CustomerDataContext customerDataContext;

        public CustomersController(IConfiguration config)
        {
            customerDataContext = new CustomerDataContext(config);
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


        [Route("PostCourses")]
        [HttpPost]

        public async Task<IActionResult> PostCustomer(CustomerModel obj)
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
    }
}

