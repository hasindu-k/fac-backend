using Microsoft.AspNetCore.Mvc;
using ITP_PROJECT.Business;
using ITP_PROJECT.Models;
using System;

namespace ITP_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDataContext _orderDataContext;

        public OrderController(IConfiguration config)
        {
            _orderDataContext = new OrderDataContext(config);
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = _orderDataContext.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] OrderModel order)
        {
            try
            {
                var addedOrder = _orderDataContext.AddOrder(order);
                if (addedOrder != null)
                {
                    return CreatedAtAction(nameof(AddOrder), addedOrder);
                }
                else
                {
                    return BadRequest("Failed to add order");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderModel order)
        {
            try
            {
                order.OrderID = id; // Set the OrderID of the supplied model to match the ID from the route

                if (_orderDataContext.UpdateOrder(order))
                {
                    return Ok("Order updated successfully");
                }
                else
                {
                    return NotFound("Order not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                if (_orderDataContext.DeleteOrder(id))
                {
                    return Ok("Order deleted successfully");
                }
                else
                {
                    return NotFound("Order not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
