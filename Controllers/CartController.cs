using log4net;
using Microsoft.AspNetCore.Mvc;
using Product.Bussiness;
using Product.Models;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private CartDataContext cartDataContext;
        private static readonly ILog Log = LogManager.GetLogger(typeof(CartController));

        public CartController(IConfiguration config)
        {
            cartDataContext = new CartDataContext(config);
        }


        [Route("GetAllCartProducts")]
        [HttpGet]
        public async Task<IActionResult> GetAllCartProducts()
        {
            try
            {
                var cartProducts = cartDataContext.GetAllCartProducts();
                return Ok(cartProducts);
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAllCartProducts", ex);
                return StatusCode(500, "Intern server error");
            }
        }

        [Route("PostCartProducts")]
        [HttpPost]

        public async Task<IActionResult> PostCartProducts(CartModel obj)
        {
            bool result = false;
            try
            {
                result = cartDataContext.PostCartProducts(obj);
            }
            catch (Exception ex)
            {
                Log.Error("Error in PostCartProducts", ex);
            }

            return Ok(result);
        }

        [Route("UpdateCartProducts")]
        [HttpPut]

        public async Task<IActionResult> UpdateCartProducts(CartModel obj)
        {
            bool result = false;
            try
            {
                result = cartDataContext.UpdateCartProducts(obj);
            }

            catch (Exception ex)
            {
                Log.Error("Error in UpdateCartProducts");
            }

            return Ok(result);
        }

        [Route("DeleteCartProducts")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCartProducts(int CId)
        {
            bool result = false;
            try
            {
                result = cartDataContext.DeleteCartProducts(CId);
            }
            catch (Exception ex)
            {
                Log.Error("Error in DeleteCartProducts");
            }

            return Ok(result);
        }
    }
}
