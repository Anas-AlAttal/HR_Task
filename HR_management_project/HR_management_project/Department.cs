using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HR_management_project
{
    internal class Department
    {
        public event EventHandler<EmployeeEventArgs> EmployeeAdded;
        public event EventHandler<EmployeeEventArgs> EmployeeRemoved;
        public event EventHandler<EmployeeEventArgs> IncreaseDepartmentBalanceRequested;

        static int departmentCounter = 0;
        public int DepartmentId { get; private set; }
        public string DepartmentName { get; private set; }
        public int EmployeeCount { get; private set; }
        public decimal Balance { get; private set; }
        public decimal TotalSalaries = 0;

        private readonly List<Employee> Employees = new List<Employee>();



        public Department( string departmentName,  decimal balance)
        {
            DepartmentId = ++departmentCounter;
            DepartmentName = SetDepartmentName(departmentName);
            EmployeeCount = SetDepartmenEmployeeCount();
            Balance = SetBalance(balance);
        }

        private int SetDepartmenEmployeeCount()
        {
            return Employees.Count;
        }

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

        public static bool IsExistedDepartment(int Id)
        {
            return Id > 0 && Id <= departmentCounter;
        }

           public void AddEmployee(Employee employee)
           {
               if(employee.DepartmentId != DepartmentId)
                 throw new ArgumentException("Employee does not belong to this department.");
                   
                if (Employees.Contains(employee))
                       throw new ArgumentException("Employee already exists in this department.");

               if (Balance < TotalSalaries + employee.NetSalary)
                {
                    IncreaseDepartmentBalanceRequested?.Invoke(this, new EmployeeEventArgs(employee));
                     return;
            }

                   Employees.Add(employee);
                   EmployeeCount = SetDepartmenEmployeeCount();
                    UpdateTotalSalaries( TotalSalaries + employee.NetSalary);
                   EmployeeAdded?.Invoke(this,new EmployeeEventArgs(employee));

            employee.HolidayRequest += (sender,e) => Notify.NotifyForRequestHoliday(employee);
           }

        public void RemoveEmployee(Employee employee) {
            if(employee.DepartmentId != DepartmentId)
                throw new ArgumentException("Employee does not belong to this department.");
            
            Employees.Remove(employee);
            EmployeeCount = SetDepartmenEmployeeCount();
            EmployeeRemoved?.Invoke(this,new EmployeeEventArgs(employee));
        }

        public void UpdateBalance(decimal amount)
            {
                if (amount < 0 && Balance + amount < 0)
                    throw new ArgumentException("Insufficient balance.");
                Balance += amount;
            }
        private void UpdateTotalSalaries(decimal amount)
        {
            if (amount < 0 && TotalSalaries + amount < 0)
                throw new ArgumentException("Total salaries cannot be negative.");
            TotalSalaries += amount;
        }
        public void UpdateDepartmentName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Department name cannot be empty.");
            DepartmentName = newName;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return Employees;
        }

        public Employee GetEmployeeById(int id)
        { 
            var employee = Employees.Find(e => e.EmployeeId == id) ?? throw new ArgumentException("Employee not found.");
            return employee;
        }
    }
}
