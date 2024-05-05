using ITP_PROJECT.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace ITP_PROJECT.Business
{
    public class SalaryDataContext : DataContext
    {
        public SalaryDataContext(IConfiguration configuration) : base(configuration)
        {

        }

        public List<EmployeeSalaryModel> GetAllEmployeeSalaries()
        {
            List<EmployeeSalaryModel> employeeSalaries = new List<EmployeeSalaryModel>();

            ExecuteScalar("SELECT * FROM EmployeeSalaries", cmd => { }, reader =>
            {
                EmployeeSalaryModel salary = new EmployeeSalaryModel();
                salary.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                salary.EmployeeName = reader["EmployeeName"].ToString();
                salary.DateOfBirth = DateOnly.FromDateTime(Convert.ToDateTime(reader["DateOfBirth"]));
                salary.PhoneNumber = reader["PhoneNumber"].ToString();
                salary.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                salary.HourlyRate = Convert.ToDouble(reader["HourlyRate"]);
                salary.HoursWorked = Convert.ToInt32(reader["HoursWorked"]);

                employeeSalaries.Add(salary);
            });

            return employeeSalaries;
        }

        public bool AddEmployeeSalary(EmployeeSalaryModel salary)
        {
            bool isSuccess = false;

            ExecuteNonQuery("INSERT INTO EmployeeSalaries (EmployeeName, DateOfBirth, PhoneNumber, DepartmentId, HourlyRate, HoursWorked) " +
                            "VALUES (@EmployeeName, @DateOfBirth, @PhoneNumber, @DepartmentId, @HourlyRate, @HoursWorked)",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@EmployeeName", salary.EmployeeName);
                                cmd.Parameters.AddWithValue("@DateOfBirth", salary.DateOfBirth);
                                cmd.Parameters.AddWithValue("@PhoneNumber", salary.PhoneNumber);
                                cmd.Parameters.AddWithValue("@DepartmentId", salary.DepartmentId);
                                cmd.Parameters.AddWithValue("@HourlyRate", salary.HourlyRate);
                                cmd.Parameters.AddWithValue("@HoursWorked", salary.HoursWorked);

                                isSuccess = true;
                            });

            return isSuccess;
        }

        public bool UpdateEmployeeSalary(EmployeeSalaryModel salary)
        {
            bool isSuccess = false;

            ExecuteNonQuery("UPDATE EmployeeSalaries SET EmployeeName = @EmployeeName, DateOfBirth = @DateOfBirth, " +
                            "PhoneNumber = @PhoneNumber, DepartmentId = @DepartmentId, HourlyRate = @HourlyRate, HoursWorked = @HoursWorked " +
                            "WHERE EmployeeId = @EmployeeId",
                            cmd =>
                            {
                                cmd.Parameters.AddWithValue("@EmployeeName", salary.EmployeeName);
                                cmd.Parameters.AddWithValue("@DateOfBirth", salary.DateOfBirth);
                                cmd.Parameters.AddWithValue("@PhoneNumber", salary.PhoneNumber);
                                cmd.Parameters.AddWithValue("@DepartmentId", salary.DepartmentId);
                                cmd.Parameters.AddWithValue("@HourlyRate", salary.HourlyRate);
                                cmd.Parameters.AddWithValue("@HoursWorked", salary.HoursWorked);
                                cmd.Parameters.AddWithValue("@EmployeeId", salary.EmployeeId);

                                isSuccess = true;
                            });

            return isSuccess;
        }

        public bool DeleteEmployeeSalary(int employeeId)
        {
            ExecuteNonQuery("DELETE FROM EmployeeSalaries WHERE EmployeeId = @EmployeeId", cmd =>
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            });

            return true;
        }
    }
}


