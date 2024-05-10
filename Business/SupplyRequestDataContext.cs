using ITP_PROJECT.Models;

namespace ITP_PROJECT.Business
{
    public class SupplyRequestDataContext : DataContext
    {
        public SupplyRequestDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<SupplyRequestModel> GetAllSupplyRequests()
        {
            List<SupplyRequestModel> supplyRequests = new List<SupplyRequestModel>();

            ExecuteScalar("SELECT RequestId, RequestedBy, RequestDate, ItemName, Quantity, Description FROM SupplyRequests", cmd => { }, reader =>
            {
                SupplyRequestModel request = new SupplyRequestModel
                {
                    RequestId = Convert.ToInt32(reader["RequestId"]),
                    RequestedBy = reader["RequestedBy"].ToString(),
                    RequestDate = Convert.ToDateTime(reader["RequestDate"]),
                    ItemName = reader["ItemName"].ToString(),
                    Quantity = Convert.ToInt32(reader["Quantity"]), 
                };
                supplyRequests.Add(request);
            });

            return supplyRequests;
        }

        public bool CreateSupplyRequest(SupplyRequestModel request)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO SupplyRequests (RequestedBy, RequestDate, ItemName, Quantity) VALUES (@RequestedBy, @RequestDate, @ItemName, @Quantity)",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@RequestedBy", request.RequestedBy);
                                cmd.Parameters.AddWithValue("@RequestDate", request.RequestDate);
                                cmd.Parameters.AddWithValue("@ItemName", request.ItemName);
                                cmd.Parameters.AddWithValue("@Quantity", request.Quantity);

                                isSuccess = true;
                            });

            return isSuccess;
        }

        public bool UpdateSupplyRequest(SupplyRequestModel request)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE SupplyRequests SET RequestedBy = @RequestedBy, RequestDate = @RequestDate, ItemName = @ItemName, Quantity = @Quantity, Description = @Description WHERE RequestId = @RequestId",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@RequestedBy", request.RequestedBy);
                                cmd.Parameters.AddWithValue("@RequestDate", request.RequestDate);
                                cmd.Parameters.AddWithValue("@ItemName", request.ItemName);
                                cmd.Parameters.AddWithValue("@Quantity", request.Quantity);
                                cmd.Parameters.AddWithValue("@RequestId", request.RequestId);

                                isSuccess = cmd.ExecuteNonQuery() > 0;
                            });

            return isSuccess;
        }

        public bool DeleteSupplyRequest(int requestId)
        {
            bool isSuccess = false;

            ExecuteNonQuery("DELETE FROM SupplyRequests WHERE RequestId = @RequestId",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@RequestId", requestId);
                                isSuccess = cmd.ExecuteNonQuery() > 0;
                            });

            return isSuccess;
        }
    }
}
