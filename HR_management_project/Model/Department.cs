using HR_management_project.Data;
using HR_management_project.Event;
using HR_management_project.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HR_management_project.Model
{
    public class Department : IEntity<int>
    {
        public event EventHandler<EmployeeEventArg> EmployeeAdded;
        public event EventHandler<EmployeeEventArg> EmployeeRemoved;



        public int Id { get; set; }
        public string DepartmentName { get; private set; }
        //public int EmployeeCount { get; private set; }
        public decimal Balance { get; private set; }

        public decimal TotalSalaries { get; private set; } = 0;



        public Department() { }

        public Department(string departmentName, decimal balance)
        {
            DepartmentName = SetDepartmentName(departmentName);
            //  EmployeeCount = GetDepartmensEmployeeCount();
            Balance = SetBalance(balance);
        }

        //private async Task<int> SetDepartmenEmployeeCount()
        //{
        //    return Employees.Count;
        //}

        private decimal SetBalance(decimal balance)
        {
            if (balance < 0)
                throw new ArgumentException("Balance cannot be negative.");
            return balance;
        }

        private string SetDepartmentName(string departmentName)
        {
            if (string.IsNullOrWhiteSpace(departmentName))
                throw new ArgumentException("Department name cannot be empty.");
            return departmentName;
        }

        /// add Employees check before Update and move to services 
        public void UpdateBalance(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Insufficient balance.");
            Balance = amount;
        }
        /// move to services
        public void UpdateDepartmentName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Department name cannot be empty.");
            DepartmentName = newName;
        }

        //public IReadOnlyList<Employee> GetEmployees()
        //{
        //    return Employees;
        //}



        public override string ToString()
        {
            return $"Department Id: {Id}, Name: {DepartmentName}";
        }
    }
}
