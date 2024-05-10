namespace ITP_PROJECT.Models
{
    public class EmployeeSalaryModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public double HourlyRate { get; set; }
        public int HoursWorked { get; set; }

    }
}
