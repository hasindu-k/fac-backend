using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TeaFactory.Models;

namespace TeaFactory.Business
{
    public class SupplierDataContext : DataContext, IDisposable
    {
        public SupplierDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<SupplierModel> GetAllsuppliers()
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();

            try
            {
                ExecuteScalar("SELECT * FROM SupplierDetails", cmd => { }, reader =>
                {
                    SupplierModel supplier = new SupplierModel();

                    supplier.SupplierID = Convert.ToInt32(reader["SupplierID"]);
                    supplier.SupplierName = reader["SupplierName"].ToString();
                    supplier.CompanyName = reader["CompanyName"].ToString();
                    supplier.JoinDate = Convert.ToDateTime(reader["JoinDate"]).Date;
                    supplier.ContactNumber = reader["ContactNumber"].ToString();
                    supplier.Email = reader["Email"].ToString();
                    supplier.Address = reader["Address"].ToString();

                    suppliers.Add(supplier);
                });
            }
            catch (Exception ex)
            {
                // Handle exception, log, or rethrow
                throw ex;
            }

            return suppliers;
        }
        //Add Supplier
        public SupplierModel AddSupplier(SupplierModel obj)
        {
            try
            {
                ExecuteNonQuery("INSERT INTO SupplierDetails (SupplierName, CompanyName, JoinDate, ContactNumber, Email, Address) " +
                                "VALUES (@SupplierName, @CompanyName, @JoinDate, @ContactNumber, @Email, @Address)",
                                cmd =>
                                {
                                    cmd.Parameters.AddWithValue("@SupplierName", obj.SupplierName);
                                    cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                                    cmd.Parameters.AddWithValue("@JoinDate", obj.JoinDate);
                                    cmd.Parameters.AddWithValue("@ContactNumber", obj.ContactNumber);
                                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                                    cmd.Parameters.AddWithValue("@Address", obj.Address);
                                });

                return obj;
            }
            catch (Exception ex)
            {
                // Handle exception, log, or rethrow
                throw ex;
            }
        }
        //Update Supplier
        public bool Updatesuppliers(SupplierModel obj)
        {
            try
            {
                ExecuteNonQuery("UPDATE SupplierDetails SET SupplierName = @SupplierName, CompanyName = @CompanyName," +
                    " ContactNumber = @ContactNumber, Email = @Email, Address = @Address WHERE SupplierID = @SupplierID",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@SupplierName", obj.SupplierName);
                        cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                        //cmd.Parameters.AddWithValue("@JoinDate", obj.JoinDate);
                        cmd.Parameters.AddWithValue("@ContactNumber", obj.ContactNumber);
                        cmd.Parameters.AddWithValue("@Email", obj.Email);
                        cmd.Parameters.AddWithValue("@Address", obj.Address);
                        cmd.Parameters.AddWithValue("@SupplierID", obj.SupplierID); // Add SupplierID parameter for WHERE clause
                    });

                return true;
            }
            catch (Exception ex)
            {
                // Handle exception, log, or return false
                return false;
            }
        }
        //Delete Supplier
        public bool Deletesupplier(int SupplierID)
        {
            try
            {
                ExecuteNonQuery("DELETE FROM SupplierDetails WHERE SupplierID = @SupplierID", cmd =>
                {
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
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
