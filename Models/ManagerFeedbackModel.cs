namespace CustomAffair.Models
{
    
    public class ManagerFeedbackModel
    {
        public int fBackID { get; set; }
        public int fBackCusID { get; set; }
        public string fBackCusName { get; set; }
        public int fBackProductID { get; set; }
        public string fBackDescription { get; set; }
        public string fBackEmail { get; set; }

        public int replyID { get; set; } // Could be null if no reply exists
        public string replyText { get; set; } // Could be null or empty string if no reply exists
    }

}
