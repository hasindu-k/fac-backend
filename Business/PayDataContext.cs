using ITP_PROJECT.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace ITP_PROJECT.Business
{
    public class PayDataContext : DataContext {
        public PayDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<PayModel> GetAllPayments()
        {
            List<PayModel> payments = new List<PayModel>();

            ExecuteScalar("select * from Transactions",
            cmd => { },
            reader =>

                {
                    PayModel payment = new PayModel();
                    payment.TransactionID = Convert.ToInt32(reader["TransactionID"]);
                    payment.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                    payment.ProductID = Convert.ToInt32(reader["ProductID"]);
                    payment.PaymentMethod = reader["PaymentMethod"].ToString();
                    payment.Amount = Convert.ToInt32(reader["Amount"]);
                    payment.Timestamp = Convert.ToDateTime(reader["Timestamp"]);
               

                    payments.Add(payment);
                } );

            return payments;
        }




        public bool PostPayment(PayModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO Transactions (TransactionID ,CustomerID, ProductID, PaymentMethod, Amount, Timestamp ) VALUES (@TransactionID,@CustomerID, @ProductID, @PaymentMethod, @Amount, GETDATE() )",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@TransactionID", obj.TransactionID);
                    cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                    cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
                    cmd.Parameters.AddWithValue("@PaymentMethod", obj.PaymentMethod);
                    cmd.Parameters.AddWithValue("@Amount", obj.Amount);
                    cmd.Parameters.AddWithValue("@Timestamp", obj.Timestamp);


                    isSuccess = true;
                });

            return isSuccess;
        }
        public bool PostTransaction(TransactionModel obj) // Renamed method name to reflect table
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO Transactions (CustomerID, ProductID, PaymentMethod, Amount, Timestamp) VALUES (@CustomerID, @ProductID, @PaymentMethod, @Amount, @Timestamp)",
              cmd =>
              {
                  cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                  cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
                  cmd.Parameters.AddWithValue("@PaymentMethod", obj.PaymentMethod);
                  cmd.Parameters.AddWithValue("@Amount", obj.Amount);
                  cmd.Parameters.AddWithValue("@Timestamp", obj.Timestamp);
              });

            isSuccess = true;

            return isSuccess;
        }
        public bool UpdateTransaction(TransactionModel obj) // Renamed method name
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE Transactions SET CustomerID = @CustomerID, ProductID = @ProductID, PaymentMethod = @PaymentMethod, Amount = @Amount, Timestamp = @Timestamp WHERE TransactionID = @TransactionID",
              cmd =>
              {
                  cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                  cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
                  cmd.Parameters.AddWithValue("@PaymentMethod", obj.PaymentMethod);
                  cmd.Parameters.AddWithValue("@Amount", obj.Amount);
                  cmd.Parameters.AddWithValue("@Timestamp", obj.Timestamp);
                  cmd.Parameters.AddWithValue("@TransactionID", obj.TransactionID); // Assuming TransactionID is the primary key

                  isSuccess = cmd.ExecuteNonQuery() > 0;
              });

            

            return isSuccess;
        }

        public bool DeleteTransaction(int transactionId)
        {
            bool isSuccess = false;

            ExecuteNonQuery("DELETE FROM Transactions WHERE TransactionID = @Id",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", transactionId);
                   
                });

            return isSuccess;
        }


       
    }




}
