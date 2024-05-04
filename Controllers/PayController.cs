using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ITP_PROJECT.Models;
using ITP_PROJECT.Business;
using Microsoft.Extensions.Configuration;

namespace ITP_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private PayDataContext payDataContext;

        public PaymentController(IConfiguration config)
        {
            payDataContext = new PayDataContext(config);
        }


        [Route("GetAllPayments")]
        [HttpGet]
        public async Task <IActionResult> GetAllPayments()
        {
            try
            {
                var payments = payDataContext.GetAllPayments();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Route("CreatePayment")]
        [HttpPost]
        public async Task<IActionResult> CreatePayment(PayModel payment)
        {
            bool result = false;
            try
            {
                result = payDataContext.PostPayment(payment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }
        
                [Route("UpdatePayment")]
                [HttpPut]
                public async Task<IActionResult> UpdatePayment(PayModel payment)
                {
                    bool result = false;
                    try
                    {
                        //result = payDataContext.UpdatePayment(payment);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return StatusCode(500, "Internal Server Error");
                    }

                    return Ok(result);
                }
                [Route("DeletePayment")]
                [HttpDelete]
                public async Task<IActionResult> DeletePayment(int paymentId)
                {
                    bool result = false;
                    try
                    {
                        result = payDataContext.DeleteTransaction(paymentId);
                result = true;
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
