using ITP_PROJECT.Business;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using ITP_PROJECT.Models;


namespace ITP_PROJECT.Business
{
    public class FeedbackCustomerDataContext : DataContext
    {
        public FeedbackCustomerDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<FeedbackModel> GetCustomerFeedbacks(int fBackCusID)
        {
            List<FeedbackModel> feedbacks = new List<FeedbackModel>();

            string query = "SELECT * FROM Feedback WHERE fBackCusID = @CustomerId";

            ExecuteScalar(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@CustomerId", fBackCusID);
            }, reader =>
            {
                FeedbackModel feedback = new FeedbackModel();
                feedback.fBackID = Convert.ToInt32(reader["fBackID"]);
                feedback.fBackCusID = Convert.ToInt32(reader["fBackCusID"]);
                feedback.fBackCusName = reader["fBackCusName"].ToString();
                feedback.fBackProductID = Convert.ToInt32(reader["fBackProductID"]);
                feedback.fBackDescription = reader["fBackDescription"].ToString();
                feedback.fBackEmail = reader["fBackEmail"].ToString();

                feedbacks.Add(feedback);
            });

            return feedbacks;
        }


        public bool PostCusFeedbacks(FeedbackModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO Feedback (fBackCusID, fBackCusName, fBackProductID, fBackDescription, fBackEmail) " +
                           "VALUES (@fBackCusID, @fBackCusName, @fBackProductID, @fBackDescription, @fBackEmail)",
                           cmd =>
                           {
                               cmd.Parameters.AddWithValue("@fBackCusID", obj.fBackCusID);
                               cmd.Parameters.AddWithValue("@fBackCusName", obj.fBackCusName);
                               cmd.Parameters.AddWithValue("@fBackProductID", obj.fBackProductID);
                               cmd.Parameters.AddWithValue("@fBackEmail", obj.fBackEmail);
                               cmd.Parameters.AddWithValue("@fBackDescription", obj.fBackDescription);

                               isSuccess = true;
                           });

            return isSuccess;
        }



        public bool UpdateCusFeedbacks(FeedbackModel obj)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE Feedback SET fBackCusID = @fBackCusID , fBackCusName = @fBackCusName , fBackProductID= @fBackProductID, fBackDescription= @fBackDescription, fBackEmail= @fBackEmail WHERE fBackID = @fBackID ", cmd =>
            {
                cmd.Parameters.AddWithValue("@fBackID", obj.fBackID);
                cmd.Parameters.AddWithValue("@fBackCusID", obj.fBackCusID);
                cmd.Parameters.AddWithValue("@fBackCusName", obj.fBackCusName);
                cmd.Parameters.AddWithValue("@fBackProductID", obj.fBackProductID);
                cmd.Parameters.AddWithValue("@fBackDescription", obj.fBackDescription);
                cmd.Parameters.AddWithValue("@fBackEmail", obj.@fBackEmail);

                isSuccess = true;
            });

            return isSuccess;
        }

        

        public bool DeleteCusFeedback(int fBackID)
        {
            try
            {
                ExecuteNonQuery("DELETE FROM Feedback WHERE fBackID = @fBackID", cmd =>
                {
                    cmd.Parameters.AddWithValue("@fBackID", fBackID);
                });

                return true; // If deletion is successful
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error)
                Console.WriteLine("Error deleting feedback: " + ex.Message);
                return false; // If an error occurs during deletion
            }
        }

    
    }
}
