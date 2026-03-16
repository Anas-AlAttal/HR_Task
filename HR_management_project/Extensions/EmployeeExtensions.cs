using HR_management_project.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Extensions
{
    internal static class EmployeeExtensions
    {
        public static int CalculateYearsOfService(this Employee employee)
            {
            var workingDays = (DateTime.UtcNow - employee.DateOfJoining).Days;
            var years = (int)(workingDays / 365.25);
            return years;
        }
    }
}
