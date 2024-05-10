using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TeaFactory.Models;

namespace TeaFactory.Business
{
    public class OrderDataContext : DataContext, IDisposable
    {
        public OrderDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<OrderModel> GetAllOrders()
        {
            List<OrderModel> orders = new List<OrderModel>();

            try
            {
                ExecuteScalar("SELECT * FROM OrderDetails", cmd => { }, reader =>
                {
                    OrderModel order = new OrderModel();

                    order.OrderID = Convert.ToInt32(reader["OrderID"]);
                    order.ItemName = reader["ItemName"].ToString();
                    order.Quantity = Convert.ToInt32(reader["Quantity"]);
                    order.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                    order.OrderState = reader["OrderState"].ToString();
                    order.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                    order.SupplierID = Convert.ToInt32(reader["SupplierID"]);

                    orders.Add(order);
                });
            }
            catch (Exception ex)
            {
                // Handle exception, log, or rethrow
                throw ex;
            }

            return orders;
        }

        // Add Order
        public OrderModel AddOrder(OrderModel obj)
        {
            try
            {
                ExecuteNonQuery("INSERT INTO OrderDetails (ItemName, Quantity, UnitPrice, OrderState, OrderDate, SupplierID) " +
                                "VALUES (@ItemName, @Quantity, @UnitPrice, @OrderState, @OrderDate, @SupplierID)",
                                cmd =>
                                {
                                    cmd.Parameters.AddWithValue("@ItemName", obj.ItemName);
                                    cmd.Parameters.AddWithValue("@Quantity", obj.Quantity);
                                    cmd.Parameters.AddWithValue("@UnitPrice", obj.UnitPrice);
                                    cmd.Parameters.AddWithValue("@OrderState", obj.OrderState);
                                    cmd.Parameters.AddWithValue("@OrderDate", obj.OrderDate);
                                    cmd.Parameters.AddWithValue("@SupplierID", obj.SupplierID);
                                });

                return obj;
            }
            catch (Exception ex)
            {
                // Handle exception, log, or rethrow
                throw ex;
            }
        }

        // Update Order
        public bool UpdateOrder(OrderModel obj)
        {
            try
            {
                ExecuteNonQuery("UPDATE OrderDetails SET ItemName = @ItemName, Quantity = @Quantity, " +
                    "UnitPrice = @UnitPrice, OrderState = @OrderState, SupplierID = @SupplierID WHERE OrderID = @OrderID",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@ItemName", obj.ItemName);
                        cmd.Parameters.AddWithValue("@Quantity", obj.Quantity);
                        cmd.Parameters.AddWithValue("@UnitPrice", obj.UnitPrice);
                        cmd.Parameters.AddWithValue("@OrderState", obj.OrderState);
                        cmd.Parameters.AddWithValue("@SupplierID", obj.SupplierID);
                        cmd.Parameters.AddWithValue("@OrderID", obj.OrderID);
                    });

                return true;
            }
            catch (Exception ex)
            {
                // Handle exception, log, or return false
                return false;
            }
        }

        // Delete Order
        public bool DeleteOrder(int OrderID)
        {
            try
            {
                ExecuteNonQuery("DELETE FROM OrderDetails WHERE OrderID = @OrderID", cmd =>
                {
                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                });

                return true;
            }
            catch (Exception ex)
            {
                // Handle exception, log, or return false
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources
                base.Dispose();
            }
        }
    }
}
