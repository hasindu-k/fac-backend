using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using ITP_PROJECT.Business;
using ITP_PROJECT.Models;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ITP_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private LeaveDataContext leaveDataContext;

        public LeaveController(IConfiguration config)
        {
            leaveDataContext = new LeaveDataContext(config);
        }

        [Route("GetAllEmployeeLeaves")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployerLeaves()
        {
            try
            {
                var leaves = leaveDataContext.GetAllEmployerLeaves();
                return Ok(leaves);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Route("AddEmployerLeave")]
        [HttpPost]
        public async Task<IActionResult> AddEmployeeLeave(EmployerLeaveModel leave)
        {
            bool result = false;
            try
            {
                result = leaveDataContext.AddEmployeeLeave(leave);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(result);
        }

        [Route("UpdateEmployeeLeave")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeLeave(EmployerLeaveModel leave)
        {
            bool result = false;
            try
            {
                result = leaveDataContext.UpdateEmployerLeave(leave);
            }
           
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }

            Console.WriteLine(result);

            return Ok(result);
        }



        [HttpDelete("DeleteEmployeeLeave/{employeeId}")]
        public IActionResult DeleteEmployeeLeave(int employeeId)
        {
            try
            {
                bool result = leaveDataContext.DeleteEmployeeLeave(employeeId);
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

