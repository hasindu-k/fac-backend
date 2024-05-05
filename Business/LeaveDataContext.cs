using ITP_PROJECT.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace ITP_PROJECT.Business
{
    public class LeaveDataContext : DataContext
    {
        public LeaveDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<EmployerLeaveModel> GetAllEmployerLeaves()
        {
            List<EmployerLeaveModel> employerLeaves = new List<EmployerLeaveModel>();

            ExecuteScalar("SELECT * FROM EmployerLeave", cmd => { }, reader =>
            {
                EmployerLeaveModel leave = new EmployerLeaveModel();
                leave.leaveId = Convert.ToInt32(reader["table_id"]);
                leave.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                leave.DepartmentId = Convert.ToInt32(reader["department_id"]);
                leave.NoofLeaveDays = Convert.ToInt32(reader["NoOfDays"]);
                leave.leave_type = reader["leave_type"].ToString();

                employerLeaves.Add(leave);
            });

            return employerLeaves;
        }

        public bool AddEmployeeLeave(EmployerLeaveModel leave)
        {
            bool isSuccess = false;

            try
            {
                // Execute the SQL query to insert the employee leave record
                ExecuteNonQuery("INSERT INTO EmployerLeave (employee_id, department_id, leave_type, NoOfDays) " +
                "VALUES (@EmployeeId, @DepartmentId, @leave_type, @NoofLeaveDays)",
                cmd =>
                {
                    // Add parameters to the SQL command
                    cmd.Parameters.AddWithValue("@EmployeeId", leave.EmployeeId);
                    cmd.Parameters.AddWithValue("@DepartmentId", leave.DepartmentId);
                    cmd.Parameters.AddWithValue("@leave_type", leave.leave_type);
                    cmd.Parameters.AddWithValue("@NoofLeaveDays", leave.NoofLeaveDays);
                });


                // If the SQL query executed without any exceptions, set isSuccess to true
                isSuccess = true;
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error)
                Console.WriteLine("Error adding employee leave: " + ex.Message);
            }

            return isSuccess;
        }


        public bool UpdateEmployerLeave(EmployerLeaveModel leave)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE EmployerLeave SET employee_id = @EmployeeId, " +
                            "department_id = @DepartmentId, leave_type = @leave_type, " +
                            "NoOfDays = @NoofLeaveDays WHERE table_id = @leaveId",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@EmployeeId", leave.EmployeeId);
                                cmd.Parameters.AddWithValue("@DepartmentId", leave.DepartmentId);
                                cmd.Parameters.AddWithValue("@leave_type", leave.leave_type);
                                cmd.Parameters.AddWithValue("@NoofLeaveDays", leave.NoofLeaveDays);
                                cmd.Parameters.AddWithValue("@leaveId", leave.leaveId);

                                isSuccess = cmd.ExecuteNonQuery() > 0;
                            });

            return isSuccess;
        }


        //this delete all leaves from employee

        
        public bool DeleteEmployeeLeave(int employeeId)
        {
            try
            {
                ExecuteNonQuery("DELETE FROM EmployerLeave WHERE employee_id = @EmployeeId", cmd =>
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                });

                return true; // If deletion is successful
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error)
                Console.WriteLine("Error deleting employee leave: " + ex.Message);
                return false; // If an error occurs during deletion
            }
        }
        



        //this delete the leave you want

        /*
        public bool DeleteEmployeeLeave(int leaveId)
        {
            try
            {
                ExecuteNonQuery("DELETE FROM EmployerLeave WHERE table_id = @leaveId", cmd =>
                {
                    cmd.Parameters.AddWithValue("@leaveId", leaveId);
                });

                return true; // If deletion is successful
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error)
                Console.WriteLine("Error deleting employee leave: " + ex.Message);
                return false; // If an error occurs during deletion
            }
        }
        */
    }
}

