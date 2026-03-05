using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project
{
    public class Employee
    {
        public event EventHandler<EmployeeEventArgs> HolidayRequest;


        private static int employeeCounter = 0;
        public int EmployeeId { get; private set; }
        public string EmployeeName { get; private set; }
        public int DepartmentId { get; private set; }
        public decimal BaseSalary { get; private set; }
        //to do : move to another class to handle salary details
        public decimal Deduction { get; private set; }
        public decimal Bonus { get; private set; }
        public decimal NetSalary => BaseSalary - Deduction + Bonus;
        public DateTime DateOfJoining { get; private set; }

        //extetion method to calculate years of service
        public int YearsOfService {
            get
            {
                var now = DateTime.UtcNow;
                int years = now.Year - DateOfJoining.Year;
                if (now.Month < DateOfJoining.Month || (now.Month == DateOfJoining.Month && now.Day < DateOfJoining.Day))
                    years--;
                return years;
            }
        }
        //extetion method to calculate holidays based on years of service
        // make it more flixible with a delegete to calculate holidays based on years of service
        public int TotalHolidays =>  YearsOfService >= 5 ? 14 : 21;

        public Employee( string employeeName, int departmentId, decimal baseSalary)
        {
            EmployeeId = ++employeeCounter;
            EmployeeName = employeeName;
            SetDepartment(departmentId);
            SetBaseSalary(baseSalary);
            Deduction = 0;
            Bonus = 0;
            DateOfJoining = DateTime.UtcNow;
        }

        private void SetDepartment(int departmentId)
        {
            if(Department.IsExistedDepartment(departmentId))
                DepartmentId = departmentId;
            else
                throw new ArgumentException("Department not existe.");
        }
        // make it more flixible with a delegete
        private void SetBaseSalary(decimal baseSalary)
        {
            if(baseSalary<100)
                throw new ArgumentException("Base salary must be at least 100.");
            
            BaseSalary = baseSalary;
           }
        public void SetDeduction(decimal deduction)
        {
           if(deduction<0)
                throw new ArgumentException("Deduction cannot be negative.");
            Deduction = deduction;
        }
        public void SetBonus(decimal bonus)
        {
            if(bonus<0)
                throw new ArgumentException("Bonus cannot be negative.");
            Bonus = bonus;
        }
        
        public decimal GetNetSalary()
        {
            return NetSalary;
        }

        public void SetStartedDate(DateTime startedAt)
        {
            if (startedAt > DateTime.UtcNow)
                throw new ArgumentException("Start date cannot be in the future.");

            DateOfJoining = startedAt;
        }

        // to do : move to another class to handle holidays details
        public void RequestHoliday()
        {
            HolidayRequest?.Invoke(this, new EmployeeEventArgs(this));
        }

    }
}
