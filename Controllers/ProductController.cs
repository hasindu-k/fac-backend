using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Bussiness;
using Product.Models;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductDataContext productDataContext;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProductController));

        public ProductController(IConfiguration config)
        {
            productDataContext = new ProductDataContext(config);
        }


        [Route("GetAllProducts")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = productDataContext.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAllProducts", ex);
                return StatusCode(500, "Intern server error");
            }
        }

        [Route("PostProducts")]
        [HttpPost]

        public async Task<IActionResult> PostProducts(ProductModel obj)
        {
            bool result = false;
            try
            {
                result = productDataContext.PostProducts(obj);
            }
            catch (Exception ex)
            {
                Log.Error("Error in PostProducts", ex);
            }

            return Ok(result);
        }

        [Route("UpdateProducts")]
        [HttpPut]

        public async Task<IActionResult> UpdateProducts(ProductModel obj)
        {
            bool result = false;
            try
            {
                result = productDataContext.UpdateProducts(obj);
            }

            catch(Exception ex)
            {
                Log.Error("Error in UpdateProducts");
            }

            return Ok(result);
        }

        [Route("DeleteProducts")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProducts(int ProductId)
        {
            bool result = false;
            try
            {
                result = productDataContext.DeleteProducts(ProductId);
            }
            catch (Exception ex)
            {
                Log.Error("Error in DeleteProducts");
            }

            return Ok(result);
        }

    }
}
