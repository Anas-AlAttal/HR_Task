using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project
{
    internal class Notify
    {
       

    
            public static void NotifyDepartment(Department department, string message)
            {
                Console.WriteLine($"Notification for Department {department.DepartmentName} (ID: {department.DepartmentId}): {message}");
            }

        public static void NotifyDepartmentEmployees(Department department, Employee employee,string massage)
        {
            foreach (var emp in department.GetEmployees())
                Notify.NotifyDepartment(department, $"An employee {employee.EmployeeName},(Id:{employee.EmployeeId}) was {massage} to your department.");
        }

        public static void NotifyCompanyeRequest(Department department, decimal amount)
        {
           Console.WriteLine($"Notification for Department {department.DepartmentName} (ID: {department.DepartmentId}): A request to increase the department balance by {amount:C} has been made.");
        }

        public static void NotifyForRequestHoliday( Employee employee)
        {
            Console.WriteLine($"Employee (Id:{employee.EmployeeId}) Request a Holiday");
        }
    }
}

