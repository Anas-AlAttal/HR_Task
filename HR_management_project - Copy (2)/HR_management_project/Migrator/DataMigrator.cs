using HR_management_project.Data;
using HR_management_project.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Migrator
{
    public static class DataMigrator
    {
        public static async Task MigrateDepartments(DepartmentService departmentService)
        {
            await departmentService.AddDepartment("hr", 100);
            await departmentService.AddDepartment("qa", 200);
            await departmentService.AddDepartment("dev", 1000);
        }

        public static async Task MigrateEmployees(EmployeeService departmentService)
        {
            await departmentService.AddEmployee("jamal",1, 100);
            await departmentService.AddEmployee("anas",1, 200);
            await departmentService.AddEmployee("mohammad",2, 1000);
        }
    }
}
