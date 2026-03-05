using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project
{
    //make it more flixible with a interface to handle notifications
    internal class Notify
    {

        // hide the implementation of the notification system, and
        // make it more flexible to change the way of notification in the future

        public static void NotifyDepartment(Department department, string message)
            {
                Console.WriteLine($"Notification for Department {department.DepartmentName} (ID: {department.DepartmentId}): {message}");
            }

        public static void NotifyDepartmentEmployees(Department department, Employee employee,string massage)
        {
            foreach (var emp in department.GetEmployees())
                Notify.NotifyDepartment(department, $"A new employee {employee.EmployeeName},(Id:{employee.EmployeeId}) was {massage} to your department.");
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
