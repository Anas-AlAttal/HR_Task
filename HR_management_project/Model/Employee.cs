using HR_management_project.Data;
using HR_management_project.Extensions;
using HR_management_project.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Model
{
    public class Employee : IEntity<int>
    {
        public string EmployeeName { get; private set; }
        public int DepartmentId { get; private set; }
        public EmployeeSalary Salary { get; private set; }
        public DateTime DateOfJoining { get; private set; }

        //extetion method to calculate years of service
        public int YearsOfService => this.CalculateYearsOfService();

        public int Id { get; set; }

        public Employee() { }
        
        public Employee(string employeeName, int departmentId, decimal baseSalary)
        {
            SetEmployeeName(employeeName);
            DepartmentId = departmentId;
            DateOfJoining = DateTime.UtcNow;
            Salary = new EmployeeSalary(baseSalary);
        }

        private void SetEmployeeName(string employeeName)
        {
            if (string.IsNullOrWhiteSpace(employeeName))
                throw new ArgumentException("Employee name cannot be empty.");
            EmployeeName = employeeName;
        }

        

        public void SetStartedDate(DateTime startedAt)
        {
            if (startedAt > DateTime.UtcNow)
                throw new ArgumentException("Start date cannot be in the future.");

            DateOfJoining = startedAt;
        }

        public decimal GetNetSalary()
        {
            return Salary.NetSalary;
        }

        /// Move to services
        public void ChangeDepartment(int departmentId)
        {
            if (DepartmentId < 0)
                throw new ArgumentException("Department not found");
            DepartmentId = departmentId;
        }
        public override string ToString()
        {
            return $"Employee Id: {Id}, Name: {EmployeeName}, DepartmentId: {DepartmentId}";
        }

    }
}
