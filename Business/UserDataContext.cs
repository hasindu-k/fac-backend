using Product.Models;
using ITP_PROJECT.Business;


namespace Product.Bussiness
{
    public class UserDataContext : DataContext
    {
        public UserDataContext(IConfiguration configuration) : base(configuration)
        {
        }

        public bool ValidateUser(ManagerModel request)
        {
            bool isValid = false;

            try
            {
                ExecuteReader("SELECT * FROM Customer WHERE UserName = @UserName",
                    cmd => { cmd.Parameters.AddWithValue("@UserName", request.UserName); },
                    reader =>
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader.GetString(reader.GetOrdinal("Password"));

                            // Compare passwords directly
                            if (storedPassword == request.password)
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
    }
}

