using Microsoft.AspNetCore.Mvc;
using ITP_PROJECT.Business;
using ITP_PROJECT.Models;

namespace ITP_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierDataContext _supplierDataContext;

        public SupplierController(IConfiguration config)
        {
            _supplierDataContext = new SupplierDataContext(config);
        }

        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            try
            {
                var suppliers = _supplierDataContext.GetAllsuppliers();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
        //Add Supplier
        [HttpPost]
        public IActionResult AddSupplier([FromBody] SupplierModel supplier)
        {
            try
            {
                var addedSupplier = _supplierDataContext.AddSupplier(supplier);
                if (addedSupplier != null)
                {
                    return CreatedAtAction(nameof(AddSupplier), addedSupplier);
                }
                else
                {
                    return BadRequest("Failed to add supplier");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
        //Update Supplier
        [HttpPut("{id}")]
        public IActionResult UpdateSupplier(int id, [FromBody] SupplierModel supplier)
        {
            try
            {
                supplier.SupplierID = id; // Set the SupplierID of the supplied model to match the ID from the route

                if (_supplierDataContext.Updatesuppliers(supplier))
                {
                    return Ok("Supplier updated successfully");
                }
                else
                {
                    return NotFound("Supplier not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
        //Delete Supplier
        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            try
            {
                if (_supplierDataContext.Deletesupplier(id))
                {
                    return Ok("Supplier deleted successfully");
                }
                else
                {
                    return NotFound("Supplier not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
