using HR_management_project.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project
{
    //make it more flixible with a interface to handle notifications
    internal class ConsoleNotify
    {

        // hide the implementation of the notification system, and make it more flexible to change the way of notification in the future

        public static void NotifyDepartment(Department department, string message)
        {
            Console.WriteLine($"Notification for Department {department.DepartmentName} (ID: {department.Id}): {message}");
        }

        //public static void NotifyDepartmentEmployees(Department department, Employee employee, string massage)
        //{
        //    foreach (var emp in department.GetEmployees())
        //        ConsoleNotify.NotifyDepartment(department, $"A new employee {employee.EmployeeName},(Id:{employee.Id}) was {massage} to your department.");
        //}

        public static void NotifyCompanyeRequest(Department department, decimal amount)
        {
            Console.WriteLine($"Notification for Department {department.DepartmentName} (ID: {department.Id}): A request to increase the department balance by {amount:C} has been made.");
        }

        public static void NotifyForRequestHoliday(Employee employee)
        {
            Console.WriteLine($"Employee (Id:{employee.Id}) Request a Holiday");
        }
    }
}
