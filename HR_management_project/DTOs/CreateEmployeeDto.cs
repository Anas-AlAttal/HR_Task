using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.DTOs
{
    public class CreateEmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentId { get; set; }
        public decimal BaseSalary { get; set; }
    }
}
