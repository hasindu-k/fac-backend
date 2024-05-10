using ITP_PROJECT.Business;
using ITP_PROJECT.Models;


namespace ITP_PROJECT.Business
{
    public class ManagerFeedbackDataContext:DataContext
    {
        public ManagerFeedbackDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<ManagerFeedbackModel> GetAllManagerlFeedbacks()
        {
            List<ManagerFeedbackModel> Feedbacks = new List<ManagerFeedbackModel>();

            // Modified query to include replyFeedback table and join with Feedback
            ExecuteScalar("SELECT f.fBackID, f.fBackCusID, f.fBackCusName, f.fBackProductID, f.fBackDescription, f.fBackEmail, r.replyText, r.replyID  " +
                          "FROM Feedback f " +
                          "LEFT JOIN replyFeedback r ON f.fBackID = r.fBackID",
                          cmd => { },
                          reader =>
                          {
                              ManagerFeedbackModel Feedback = new ManagerFeedbackModel();
                              Feedback.fBackID = Convert.ToInt32(reader["fBackID"]);
                              Feedback.fBackCusID = Convert.ToInt32(reader["fBackCusID"]);
                              Feedback.fBackCusName = reader["fBackCusName"].ToString();
                              Feedback.fBackProductID = Convert.ToInt32(reader["fBackProductID"]);
                              Feedback.fBackDescription = reader["fBackDescription"].ToString();
                              Feedback.fBackEmail = reader["fBackEmail"].ToString();

                              // Check if replyText exists (might be null for entries without replies)
                              if (reader["replyText"] != DBNull.Value)
                              {
                                  Feedback.replyText = reader["replyText"].ToString();
                              }
                              else
                              {
                                  Feedback.replyText = ""; // Assuming empty string for no replies
                              }

                              // Similar check for replyID (might be null)
                              if (reader["replyID"] != DBNull.Value)
                              {
                                  Feedback.replyID = Convert.ToInt32(reader["replyID"]);
                              }
                              else
                              {
                                  Feedback.replyID = 0; // Assuming 0 for no replies (adjust based on your logic)
                              }

                              Feedbacks.Add(Feedback);
                          });

            return Feedbacks;
        }

        public bool UpdateManagerFeedback(ManagerFeedbackModel obj) // Update method with relevant name
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE replyFeedback SET replyText = @replyText " +
                            "WHERE replyID = @replyID",
                            cmd =>
                            {
                                
                                cmd.Parameters.AddWithValue("@replyText", obj.replyText);
                                cmd.Parameters.AddWithValue("@replyID", obj.replyID);

                                isSuccess = cmd.ExecuteNonQuery() > 0; // Check if rows affected > 0 (updated)
                            });

            return isSuccess;
        }

    }
}
