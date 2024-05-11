using ITP_PROJECT.Controllers;
using ITP_PROJECT.Models;

namespace ITP_PROJECT.Business
{
    public class UserDataContext : DataContext
    {
        private readonly IConfiguration _configuration;
        public UserDataContext(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public UserModel Authenticate(string managerID, string managerPassword)
        {
            UserModel user = null;
            ExecuteScalar("SELECT * FROM manager WHERE managerID = @managerID AND managerPassword = @managerPassword",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@managerID", managerID);
                    cmd.Parameters.AddWithValue("@managerPassword", managerPassword);
                },
                reader =>
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            managerID = reader["managerID"].ToString(),
                            managerName = reader["managerName"].ToString(),
                            managerNIC = reader["NIC"].ToString(),
                            managerEmail = reader["managerEmail"].ToString(),
                            managerPhone = reader["managerPhone"].ToString(),
                            managerType = reader["managerType"].ToString(),
                            managerPassword = reader["managerPassword"].ToString(),
                            // Set username and password

                        };
                    }
                });
            return user;
        }

        public List<UserModel> GetAllManagers()
        {
            List<UserModel> Managers = new List<UserModel>();

            ExecuteScalar("Select * from manager", cmd => { }, reader =>
            {
                UserModel manager = new UserModel();
                manager.managerID = reader["managerID"].ToString();
                manager.managerName = reader["managerName"].ToString();
                manager.managerNIC = reader["NIC"].ToString();
                manager.managerEmail = reader["managerEmail"].ToString();
                manager.managerPhone = reader["managerPhone"].ToString();
                manager.managerType = reader["managerType"].ToString();
                manager.managerPassword = reader["managerPassword"].ToString();

                Managers.Add(manager);
            });

            return Managers;
        }


        public UserModel GetUserDetails(string userID)
        {
            UserModel user = null;
            ExecuteScalar("SELECT * FROM Manager WHERE managerID = @managerID",
                cmd => { cmd.Parameters.AddWithValue("@managerID", userID); },
                reader =>
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            managerID = reader["managerID"].ToString(),
                            managerName = reader["managerName"].ToString(),
                            managerNIC = reader["NIC"].ToString(),
                            managerEmail = reader["managerEmail"].ToString(),
                            managerPhone = reader["managerPhone"].ToString(),
                            managerType = reader["managerType"].ToString(),
                            managerPassword = reader["managerPassword"].ToString()
                        };
                    }
                });
            return user;
        }

        public bool PostManagers(UserModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO manager (managerID, managerName, NIC, managerEmail, managerPhone, managerType, managerPassword) " +
                            "VALUES (@managerID, @managerName, @managerNIC, @managerEmail, @managerPhone, @managerType, @managerPassword)",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@managerID", obj.managerID);
                                cmd.Parameters.AddWithValue("@managerName", obj.managerName);
                                cmd.Parameters.AddWithValue("@managerNIC", obj.managerNIC);
                                cmd.Parameters.AddWithValue("@managerEmail", obj.managerEmail);
                                cmd.Parameters.AddWithValue("@managerPhone", obj.managerPhone);
                                cmd.Parameters.AddWithValue("@managerType", obj.managerType);
                                cmd.Parameters.AddWithValue("@managerPassword", obj.managerPassword);

                                isSuccess = true;
                            });

            return isSuccess;
        }

        public bool UpdateManagers(UserModel updatedUser)
        {
            bool isSuccess = false;
            ExecuteNonQuery("UPDATE manager SET managerName = @managerName, managerEmail = @managerEmail, managerPhone = @managerPhone, managerType = @managerType WHERE managerID = @managerID",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@managerID", updatedUser.managerID);
                    cmd.Parameters.AddWithValue("@managerName", updatedUser.managerName);
                    cmd.Parameters.AddWithValue("@managerEmail", updatedUser.managerEmail);
                    cmd.Parameters.AddWithValue("@managerPhone", updatedUser.managerPhone);
                    cmd.Parameters.AddWithValue("@managerType", updatedUser.managerType);

                    bool isSuccess = true;
                });
            return isSuccess; // Assuming update always succeeds
        }

        public bool DeleteUser(string managerID)
        {
            ExecuteNonQuery("DELETE FROM Manager WHERE managerID = @managerID",
                cmd => { cmd.Parameters.AddWithValue("@managerID", managerID); });
            return true; // Assuming delete always succeeds
        }



    }
}
