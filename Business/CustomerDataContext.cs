using ITP_PROJECT.Business;
using ITP_PROJECT.Models;

namespace ITP_PROJECT.Business
{
    public class CustomerDataContext : DataContext
    {
        public CustomerDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public bool ValidateCustomer(CustomerLoginRequest request)
        {
            bool isValid = false;

            try
            {
                ExecuteReader("SELECT * FROM customer WHERE cusID = @cusID",
                    cmd => { cmd.Parameters.AddWithValue("@cusID", request.cusID); },
                    reader =>
                    {
                        if (reader.Read())
                        {
                            string storedHashedPassword = reader.GetString(reader.GetOrdinal("cusPassword"));
                            string hashedPassword = HashPassword(request.cusPassword);

                            if (storedHashedPassword == hashedPassword)
                            {
                                isValid = true;
                            }
                        }
                    });

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isValid;
        }

        public List<CustomerModel> GetAllCustomers()
        {
            List<CustomerModel> Customers = new List<CustomerModel>();

            ExecuteScalar("Select * from Customer", cmd => { }, reader =>
            {
                CustomerModel customer = new CustomerModel();
                customer.cusID = Convert.ToInt32(reader["cusID"]);
                customer.cusName = reader["cusName"].ToString();
                customer.cusEmail = reader["cusEmail"].ToString();
                customer.cusDOB = Convert.ToDateTime(reader["cusDOB"]);
                customer.cusAddress = reader["cusAddress"].ToString();
                customer.cusPhone = reader["cusPhone"].ToString();
                customer.cusPassword = reader["cusPassword"].ToString();

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

                                isSuccess = true;
                            });

            return isSuccess;
        }

        public bool UpdateCustomers(CustomerModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE customer SET cusName = @cusName, cusEmail = @cusEmail, " +
                            "cusDOB = @cusDOB, cusAddress = @cusAddress, cusPhone = @cusPhone, cusPassword = @cusPassword WHERE cusID = @cusID",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@cusName", obj.cusName);
                                cmd.Parameters.AddWithValue("@cusEmail", obj.cusEmail);
                                cmd.Parameters.AddWithValue("@cusDOB", obj.cusDOB);
                                cmd.Parameters.AddWithValue("@cusAddress", obj.cusAddress);
                                cmd.Parameters.AddWithValue("@cusPhone", obj.cusPhone);
                                cmd.Parameters.AddWithValue("@cusPassword", obj.cusPassword);
                                cmd.Parameters.AddWithValue("@cusID", obj.cusID);

                                isSuccess = true;
                            });

            return isSuccess;

        }

        public bool DeleteCustomer(int cusID)
        {
            ExecuteNonQuery("DELETE FROM customer WHERE cusID = @cusID", cmd =>
            {
                cmd.Parameters.AddWithValue("@cusID", cusID);
            });

            return true;
        }


        public List<CustomerModel> GetCustomerDetails(int cusID)
        {
            List<CustomerModel> Customers = new List<CustomerModel>(cusID);

            ExecuteScalar("SELECT * FROM customer WHERE cusID = @cusID",
                cmd => { cmd.Parameters.AddWithValue("@cusID", cusID); },
                reader =>
                {
                    CustomerModel customer = new CustomerModel();
                    customer.cusID = Convert.ToInt32(reader["cusID"]);
                    customer.cusName = reader["cusName"].ToString();
                    customer.cusEmail = reader["cusEmail"].ToString();
                    customer.cusDOB = Convert.ToDateTime(reader["cusDOB"]);
                    customer.cusAddress = reader["cusAddress"].ToString();
                    customer.cusPhone = reader["cusPhone"].ToString();
                    customer.cusPassword = reader["cusPassword"].ToString();

                    Customers.Add(customer);
                });
            return Customers;
        }

        // Utility method to hash the password (you may want to use a more secure hashing mechanism)
        private string HashPassword(string cusPassword)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(cusPassword));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


    }

}
