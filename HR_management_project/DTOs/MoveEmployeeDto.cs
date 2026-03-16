using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.DTOs
{
    public class MoveEmployeeDto
    {
        public int EmployeeId { get; set; }
        public int FromDepartmentId { get; set; }
        public int ToDepartmentId { get;set; }
    }
}
