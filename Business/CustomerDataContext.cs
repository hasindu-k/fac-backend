using ITP_PROJECT.Business;
using ITP_PROJECT.Models;

namespace ITP_PROJECT.Business
{
    public class CustomerDataContext : DataContext
    {
        public CustomerDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<CustomerModel> GetAllCustomers()
        {
            List<CustomerModel> Customers = new List<CustomerModel>();

            ExecuteScalar("Select * from customer", cmd => { }, reader =>
            {
                CustomerModel customer = new CustomerModel();
                customer.cusID = Convert.ToInt32(reader["cusID"]); // Match column name
                customer.cusName = reader["cusName"].ToString(); // Match column name
                customer.cusEmail = reader["cusEmail"].ToString(); // Match column name
                customer.cusDOB = Convert.ToDateTime(reader["cusDOB"]); // No need for DateOnly conversion
                customer.cusAddress = reader["cusAddress"].ToString(); // Match column name
                customer.cusPhone = reader["cusPhone"].ToString(); // Match column name
                customer.cusPassword = reader["cusPassword"].ToString(); // Match column name

                Customers.Add(customer);

            });

            return Customers;
        }

        public bool PostCustomers(CustomerModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO customer (cusName, cusEmail, cusDOB, cusAddress, cusPhone, cusPassword) " +
                            "VALUES (@cusName, @cusEmail, @cusDOB, @cusAddress, @cusPhone, @cusPassword)",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@cusName", obj.cusName);
                                cmd.Parameters.AddWithValue("@cusEmail", obj.cusEmail);
                                cmd.Parameters.AddWithValue("@cusDOB", obj.cusDOB);
                                cmd.Parameters.AddWithValue("@cusAddress", obj.cusAddress);
                                cmd.Parameters.AddWithValue("@cusPhone", obj.cusPhone);
                                cmd.Parameters.AddWithValue("@cusPassword", obj.cusPassword);

                                bool isSuccess = true;
                            });

            return isSuccess;
        }

        public bool UpdateCustomers(CustomerModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE customer SET CusName = @CusName, cusEmail = @cusEmail, " +
                            "cusDOB = @cusDOB, cusAddress = @cusAddress, cusPhone = @cusPhone, cusPassword = @cusPassword WHERE CusID = @CusID",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@cusName", obj.cusName);
                                cmd.Parameters.AddWithValue("@cusEmail", obj.cusEmail);
                                cmd.Parameters.AddWithValue("@cusDOB", obj.cusDOB);
                                cmd.Parameters.AddWithValue("@cusAddress", obj.cusAddress);
                                cmd.Parameters.AddWithValue("@cusPhone", obj.cusPhone);
                                cmd.Parameters.AddWithValue("@cusPassword", obj.cusPassword);

                                isSuccess = true;
                            });

            return isSuccess;

        }

        public bool DeleteCustomer(int CusID)
        {
            ExecuteNonQuery("DELETE FROM customer WHERE CusID = @CusID", cmd =>
            {
                cmd.Parameters.AddWithValue("@CourseId", CusID);
            });

            return true;
        }


    }

}
