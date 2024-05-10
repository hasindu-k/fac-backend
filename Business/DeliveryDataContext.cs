using ITP_PROJECT.Models;
using ITP_PROJECT.Business;


namespace ITP_PROJECT.Business
{
    public class DeliveryDataContext : DataContext
    {
        public DeliveryDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<DeliveryModel> GetAlldeliveries()
        {
            List<DeliveryModel> deliveries = new List<DeliveryModel>();

            ExecuteScalar("SELECT * FROM delivery", cmd => { }, reader =>
            {
                DeliveryModel del = new DeliveryModel();

                del.deliveryId = Convert.ToInt32(reader["deliveryId"]);
                del.deliveryDate = Convert.ToDateTime(reader["deliveryDate"]);
                del.driverId = reader["driverId"].ToString();
                del.CusName = reader["CusName"].ToString();
                del.CusContact = reader["CusContact"].ToString();
                del.Cusaddress = reader["Cusaddress"].ToString();
                del.delStatus = reader["delStatus"].ToString();


                deliveries.Add(del);
            });

            return deliveries;
        }

        public bool Postdeliveries(DeliveryModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO delivery (deliveryDate, driverId, CusName, CusContact, Cusaddress, delStatus ) " +
                            "VALUES (@deliveryDate, @driverId, @CusName, @CusContact, @Cusaddress, @delStatus)",
                            cmd =>
                            {

                                cmd.Parameters.AddWithValue("@deliveryDate", obj.deliveryDate);
                                cmd.Parameters.AddWithValue("@driverId", obj.driverId);
                                cmd.Parameters.AddWithValue("@CusName", obj.CusName);
                                cmd.Parameters.AddWithValue("@CusContact", obj.CusContact);
                                cmd.Parameters.AddWithValue("@Cusaddress", obj.Cusaddress);
                                cmd.Parameters.AddWithValue("@delStatus", obj.delStatus);


                                isSuccess = true;

                            });

            return isSuccess;
        }

        public bool Updatedeliveries(DeliveryModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE delivery SET deliveryDate = @deliveryDate, driverId = @driverId, " +
                            "CusName = @CusName, CusContact = @CusContact, Cusaddress = @Cusaddress, delStatus = @delStatus  WHERE deliveryId = @deliveryId",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@deliveryDate", obj.deliveryDate);
                                cmd.Parameters.AddWithValue("@driverId", obj.driverId);
                                cmd.Parameters.AddWithValue("@deliveryId", obj.deliveryId);
                                cmd.Parameters.AddWithValue("@CusName", obj.CusName);
                                cmd.Parameters.AddWithValue("@CusContact", obj.CusContact);
                                cmd.Parameters.AddWithValue("@Cusaddress", obj.Cusaddress);
                                cmd.Parameters.AddWithValue("@delStatus", obj.delStatus);


                                isSuccess = true;
                            });

            return isSuccess;

        }

        public bool Deletedelivery(int deliveryId)
        {
            ExecuteNonQuery("DELETE FROM delivery WHERE deliveryId = @deliveryId", cmd =>
            {
                cmd.Parameters.AddWithValue("@deliveryId", deliveryId);
            });

            return true;
        }


    }
}
