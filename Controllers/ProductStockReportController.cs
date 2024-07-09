using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Bussiness;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStockReportController : ControllerBase
    {
        private ProductStockReportDataContext productStockReportDataContext;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProductStockReportController));

        public ProductStockReportController(IConfiguration config)
        {
            productStockReportDataContext = new ProductStockReportDataContext(config);
        }

        [Route("GetAvailableStockReport")]
        [HttpGet]
        public async Task<IActionResult> GetAvailableStockReport()
        {
            try
            {
                var availableStockReport = productStockReportDataContext.GetAvailableStockReport();
                return Ok(availableStockReport);
            }
            catch (Exception ex)
            {
                Log.Error("Error generating available stock report", ex);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}

