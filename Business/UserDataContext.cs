using ITP_PROJECT.Controllers;
using ITP_PROJECT.Models;

namespace ITP_PROJECT.Business
{
    public class UserDataContext : DataContext
    {
        public UserDataContext(IConfiguration configuration) : base(configuration)
        {
        }

        public bool ValidateUser(UserLoginRequest request)
        {
            bool isValid = false;

            try
            {
                ExecuteReader("SELECT * FROM Manager WHERE managerID = @managerID",
                    cmd => { cmd.Parameters.AddWithValue("@managerID", request.managerID); },
                    reader =>
                    {
                        if (reader.Read())
                        {
                            string storedHashedPassword = reader.GetString(reader.GetOrdinal("Password"));
                            string hashedPassword = HashPassword(request.managerPassword);

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

        internal object ValidateUser(UserLoginRequest request)
        {
            throw new NotImplementedException();
        }

        // Utility method to hash the password (you may want to use a more secure hashing mechanism)
        private string HashPassword(string managerPassword)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(managerPassword));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
