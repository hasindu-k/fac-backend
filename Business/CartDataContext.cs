using ITP_PROJECT.Business;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Product.Models;

namespace Product.Bussiness
{
    public class CartDataContext : DataContext
    {
        public CartDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<CartModel> GetAllCartProducts()
        {
            List<CartModel> CartProducts = new List<CartModel>();

            ExecuteScalar("SELECT * FROM cart", cmd => { }, reader =>
            {
                CartModel cartProducts = new CartModel();
                cartProducts.cId = Convert.ToInt32(reader["cId"]);
                cartProducts.ProductId = Convert.ToInt32(reader["pId"]);
                cartProducts.ProductName = reader["pName"].ToString();
                cartProducts.quantity = Convert.ToInt32(reader["quantity"]);

                CartProducts.Add(cartProducts);
            });
            return CartProducts;
        }

        public bool PostCartProducts(CartModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO cart (pId, pName,quantity) " +
                            "VALUES (@ProductId, @ProductName, @quantity)",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
                                cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
                                  cmd.Parameters.AddWithValue("@quantity", obj.quantity);

                                bool isSuccess = true;
                            });

            return isSuccess;
        }

        public bool UpdateCartProducts(CartModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE cart SET quantity = @quantity WHERE cId = @CartId",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@CartId", obj.cId);
                    cmd.Parameters.AddWithValue("@quantity", obj.quantity);

                    bool isSuccess = true;
                });
            return isSuccess;
        }

        public bool DeleteCartProducts(int Cid)
        {
            bool isSuccess = false;

            ExecuteNonQuery("DELETE FROM cart WHERE cId = @cId",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@cId", Cid);
                });
            return isSuccess;
        }
    }
}
