using ITP_PROJECT.Business;
using ITP_PROJECT.Models;
using Product.Models;


namespace Product.Bussiness
{
    public class ProductStockReportDataContext : DataContext
    {
        public ProductStockReportDataContext(IConfiguration configuration) : base(configuration)
        {

        }
        public List<ProductStockReportModel> GetAvailableStockReport()
        {
            List<ProductStockReportModel> Report = new List<ProductStockReportModel>();

            ExecuteScalar("SELECT ProductId, ProductName, AvaiStock FROM product", cmd => { }, reader =>
            {
                ProductStockReportModel report = new ProductStockReportModel();
                report.ProductId = Convert.ToInt32(reader["ProductId"]);
                report.ProductName = reader["ProductName"].ToString();
                report.AvaiStock = Convert.ToInt32(reader["AvaiStock"]);

                Report.Add(report);
            });
            return Report;
        }

    }
}
