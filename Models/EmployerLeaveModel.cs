namespace ITP_PROJECT.Models
{
    public class EmployerLeaveModel
    {
        public int leaveId { get; set; } // Primary key auto-incremented by the database
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int NoofLeaveDays { get; set; }
        public string leave_type { get; set; }
    }
}

