using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using ITP_PROJECT.Business;
using ITP_PROJECT.Models;

namespace TestCrud.Controllers
{
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private SalaryDataContext salaryDataContext;

        public SalaryController(IConfiguration config)
        {
            salaryDataContext = new SalaryDataContext(config);
        }

        [Route("GetAllEmployeeSalaries")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeSalaries()
        {
            try
            {
                var salaries = salaryDataContext.GetAllEmployeeSalaries();
                return Ok(salaries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Route("AddEmployeeSalary")]
        [HttpPost]
        public async Task<IActionResult> AddEmployeeSalary(EmployeeSalaryModel salary)
        {
            bool result = false;
            try
            {
                result = salaryDataContext.AddEmployeeSalary(salary);
            }
            catch (Exception ex)
            {
              
            }

            return Ok(result);
        }

        [Route("UpdateEmployeeSalary")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeSalary(EmployeeSalaryModel salary)
        {
            bool result = false;
            try
            {
                result = salaryDataContext.UpdateEmployeeSalary(salary);
            }
            catch (Exception ex)
            {
    
            }
            return Ok(result);
        }

        [Route("DeleteEmployeeSalary")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeSalary(int employeeId)
        {
            bool result = false;
            try
            {
                result = salaryDataContext.DeleteEmployeeSalary(employeeId);
            }
            catch (Exception ex)
            {
 
            }
            return Ok(result);
        }
    }
}



