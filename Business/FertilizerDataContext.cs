using ITP_PROJECT.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ITP_PROJECT.Business
{
    public class FertilizerDataContext:DataContext
    {
        public FertilizerDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<FertilizerModel> GetAllFertilizers()
        {
            List<FertilizerModel> Fertilizers = new List<FertilizerModel>();

            ExecuteScalar("SELECT FId, FName, Description, ApplicationMethod, UnitPrice, MeasurementUnit, StockQuantity, CreationDate, LastUpdate FROM fertilizerList", cmd => { }, reader =>
            {
                FertilizerModel fertilizer = new FertilizerModel();
                fertilizer.FId = Convert.ToInt32(reader["FId"]);
                fertilizer.FName = reader["FName"].ToString();
                fertilizer.Description = reader["Description"].ToString();
                fertilizer.ApplicationMethod = reader["ApplicationMethod"].ToString();
                fertilizer.UnitPrice = Convert.ToInt32(reader["UnitPrice"]);
                fertilizer.MeasurementUnit = reader["MeasurementUnit"].ToString();
                fertilizer.StockQuantity = Convert.ToInt32(reader["StockQuantity"]);
                fertilizer.CreationDate = Convert.ToDateTime(reader["CreationDate"]).Date;
                fertilizer.LastUpdate = Convert.ToDateTime(reader["LastUpdate"]);

                Fertilizers.Add(fertilizer);
            });
             
            return Fertilizers;
        }



        public bool PostFertilizer(FertilizerModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO fertilizerList (FName, Description, ApplicationMethod, UnitPrice, MeasurementUnit, StockQuantity, CreationDate, LastUpdate) VALUES (@FName, @Description, @ApplicationMethod, @UnitPrice, @MeasurementUnit, @StockQuantity, CAST(GETDATE() AS DATE) , GETDATE() )", // @CreationDate , @LastUpdate
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@FName", obj.FName);
                                cmd.Parameters.AddWithValue("@Description", obj.Description);
                                cmd.Parameters.AddWithValue("@ApplicationMethod", obj.ApplicationMethod);
                                cmd.Parameters.AddWithValue("@UnitPrice", obj.UnitPrice);
                                cmd.Parameters.AddWithValue("@MeasurementUnit", obj.MeasurementUnit);
                                cmd.Parameters.AddWithValue("@StockQuantity", obj.StockQuantity);
                                //cmd.Parameters.AddWithValue("@CreationDate", obj.CreationDate);
                                //cmd.Parameters.AddWithValue("@LastUpdate", obj.LastUpdate);

                                isSuccess = true;
                            });

            return isSuccess;
        }


        public bool UpdateFertilizer(FertilizerModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE fertilizerList SET FName = @FName, Description = @Description, ApplicationMethod = @ApplicationMethod, UnitPrice = @UnitPrice, MeasurementUnit = @MeasurementUnit, StockQuantity = @StockQuantity, LastUpdate = GETDATE() WHERE FId = @Id",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@FName", obj.FName);
                                cmd.Parameters.AddWithValue("@Description", obj.Description);
                                cmd.Parameters.AddWithValue("@ApplicationMethod", obj.ApplicationMethod);
                                cmd.Parameters.AddWithValue("@UnitPrice", obj.UnitPrice);
                                cmd.Parameters.AddWithValue("@MeasurementUnit", obj.MeasurementUnit);
                                cmd.Parameters.AddWithValue("@StockQuantity", obj.StockQuantity);
                                //cmd.Parameters.AddWithValue("@LastUpdate", obj.LastUpdate);
                                cmd.Parameters.AddWithValue("@Id", obj.FId);

                                // Set isSuccess to true upon successful execution
                                isSuccess = cmd.ExecuteNonQuery() > 0;
                            });

            return isSuccess;
        }


        public bool DeleteFertilizer(int fertilizerId)
        {
            bool isSuccess = false;

            ExecuteNonQuery("DELETE FROM fertilizerList WHERE FId = @Id",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@Id", fertilizerId);

                                // Set isSuccess to true upon successful execution
                                isSuccess = cmd.ExecuteNonQuery() > 0;
                            });

            return isSuccess;
        }


    }
}
