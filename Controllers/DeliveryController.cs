using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using teaFactory.Business;
using teaFactory.Models;

namespace teaFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private DeliveryDataContext DeliveryDataContext;

        public DeliveryController(IConfiguration config)
        {
            DeliveryDataContext = new DeliveryDataContext(config);
        }
        [Route("GetAlldeliveries")] 
        [HttpGet]

        public async Task<IActionResult> GetAlldeliveries()
        {
            try
            {
                var deliveries = DeliveryDataContext.GetAlldeliveries();
                return Ok(deliveries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [Route("Postdeliveries")]
        [HttpPost]

        public async Task<IActionResult> Postdeliveries(DeliveryModel obj)
        {
            bool result = false;
            try
            {
                result = DeliveryDataContext.Postdeliveries(obj);
            }
            catch (Exception ex)
            {
                // log.Error("Error in PostDeliveries", ex);
            }

            return Ok(result);
        }

        [Route("Updatedeliveries")]
        [HttpPut]

        public async Task<IActionResult> Updatedeliveries(DeliveryModel obj)
        {
            bool result = false;
            try
            {
                result = DeliveryDataContext.Updatedeliveries(obj);
            }
            catch (Exception ex)
            {
                // log.Error("Error in Updatedeliveries", ex);
            }

            return Ok(result);
        }

        [Route("Deletedelivery")]
        [HttpDelete]
        public async Task<IActionResult> Deletedelivery(int deliveryId)
        {
            bool result = false;
            try
            {
                result = DeliveryDataContext.Deletedelivery(deliveryId);
            }
            catch (Exception ex)
            {
                //log.Error("Error in Deletedelivery", ex);
            }

            return Ok(result);
        }

    }
}

