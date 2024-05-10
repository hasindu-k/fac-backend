using System.Data.SqlClient;

namespace TeaFactory.Business
{
    public class DataContext
    {
        private readonly IConfiguration _configuration;
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            conn.Open();
            return conn;
        }

        public void ExecuteNonQuery(string query, Action<SqlCommand> addParameters)
        {
            using (SqlConnection conn = OpenConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    addParameters(cmd);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ExecuteScalar(string query, Action<SqlCommand> addParameters, Action<SqlDataReader> processReader)
        {
            using (SqlConnection conn = OpenConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    addParameters(cmd);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            processReader(reader);
                        }
                    }
                }
            }
        }

        protected void ExecuteReader(string query, Action<SqlCommand> configureCommand, Action<SqlDataReader> readData)
        {
            using (SqlConnection conn = OpenConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    configureCommand?.Invoke(cmd);
                    readData?.Invoke(cmd.ExecuteReader());
                }
            }
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
