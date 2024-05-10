using ITP_PROJECT.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ITP_PROJECT.Business
{
    public class DriverDataContext : DataContext
    {
        public DriverDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<DriverModel> GetAlldrivers()
        {
            List<DriverModel> drivers = new List<DriverModel>();

            ExecuteScalar("SELECT * FROM driver", cmd => { }, reader =>
            {
                DriverModel driver = new DriverModel();
                driver.driverId = reader["driverId"].ToString();
                driver.driverName = reader["driverName"].ToString();
                driver.driverContact = reader["driverContact"].ToString();
                driver.driverEmail = reader["driverEmail"].ToString();
                driver.diverLic = reader["diverLic"].ToString();
                driver.driverAddress = reader["driverAddress"].ToString();
                driver.driverSal = Convert.ToDouble(reader["driverSal"]);
                

                drivers.Add(driver);
            });

            return drivers;
        }

        public bool Postdrivers(DriverModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO driver (driverId, driverName, driverContact, driverEmail, driverAddress, driverSal, diverLic ) " +
                            "VALUES (@driverId, @driverName, @driverContact, @driverEmail, @driverAddress, @driverSal, @diverLic )",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@driverId", obj.driverId);
                                cmd.Parameters.AddWithValue("@driverName", obj.driverName);
                                cmd.Parameters.AddWithValue("@driverContact", obj.driverContact);
                                cmd.Parameters.AddWithValue("@driverEmail", obj.driverEmail);
                                cmd.Parameters.AddWithValue("@driverAddress", obj.driverAddress);
                                cmd.Parameters.AddWithValue("@driverSal", obj.driverSal);
                                cmd.Parameters.AddWithValue("@diverLic", obj.diverLic);

                                isSuccess = true; // Fixed variable declaration, removed duplicate declaration
                            });

            return isSuccess;
        }

        public bool Updatedrivers(DriverModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE driver SET driverName = @driverName, driverContact = @driverContact, driverEmail = @driverEmail, " +
                            "driverAddress = @driverAddress,  diverLic = @diverLic WHERE driverId = @driverId",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@driverId", obj.driverId);
                                cmd.Parameters.AddWithValue("@driverName", obj.driverName);
                                cmd.Parameters.AddWithValue("@driverContact", obj.driverContact);
                                cmd.Parameters.AddWithValue("@driverEmail", obj.driverEmail);
                                cmd.Parameters.AddWithValue("@driverAddress", obj.driverAddress);
                               
                                cmd.Parameters.AddWithValue("@diverLic", obj.diverLic);

                                isSuccess = true;
                            });

            return isSuccess;
        }

        public bool Deletedriver(String driverId)
        {
            ExecuteNonQuery("DELETE FROM driver WHERE driverId = @driverId", cmd =>
            {
                cmd.Parameters.AddWithValue("@driverId", driverId);
            });

            return true;
        }
    }
}
