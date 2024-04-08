using ITP_PROJECT.Business;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Product.Models;


namespace Product.Bussiness
{
    public class ProductDataContext : DataContext
    {
        public ProductDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> Products = new List<ProductModel>();

            ExecuteScalar("SELECT * FROM product", cmd => { }, reader =>
            {
                ProductModel product = new ProductModel();
                product.ProductPhoto = reader["productPhoto"].ToString();
                product.ProductId = Convert.ToInt32(reader["pId"]);
                product.ProductName = reader["pName"].ToString();
                product.pDescription = reader["pDescription"].ToString();
                product.AvaiStock = Convert.ToInt32(reader["avaiStock"]);
                product.Price = Convert.ToDouble(reader["price"]);

                Products.Add(product);
            });
            return Products;
        }

        public bool PostProducts(ProductModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO product (productPhoto, pName, pDescription, avaiStock , price) " +
                            "VALUES (@ProductPhoto, @ProductName, @pDescription, @AvaiStock, @Price)",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@ProductPhoto", obj.ProductPhoto);
                                cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
                                cmd.Parameters.AddWithValue("@pDescription", obj.pDescription);
                                cmd.Parameters.AddWithValue("@AvaiStock", obj.AvaiStock);
                                cmd.Parameters.AddWithValue("@Price", obj.Price);

                                bool isSuccess = true;
                            });

            return isSuccess;
        }

        public bool UpdateProducts(ProductModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE product SET productPhoto = @ProductPhoto, pName = @ProductName, pDescription = @pDescription , avaiStock = @AvaiStock , price = @Price where pId =@ProductId",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@ProductPhoto", obj.ProductPhoto);
                    cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
                    cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
                    cmd.Parameters.AddWithValue("@pDescription", obj.pDescription);
                    cmd.Parameters.AddWithValue("@AvaiStock", obj.AvaiStock);
                    cmd.Parameters.AddWithValue("@Price", obj.Price);

                    bool isSuccess = true;
                });
            return isSuccess;
        }

        public bool DeleteProducts(int ProductId)
        {
            bool isSuccess = false;

            ExecuteNonQuery("DELETE FROM product WHERE pId = @ProductId",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@ProductId",ProductId);
                });
            return isSuccess;
        }
    }
}
