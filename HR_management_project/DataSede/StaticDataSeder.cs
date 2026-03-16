using HR_management_project.Services;

namespace HR_management_project.Migrator
{
    internal class StaticDataSeder
    {
        public static async Task MigrateDepartments(DepartmentService departmentService)
        {
            if ((await departmentService.GetList()).Any())
            {
                return;
            }
            await departmentService.AddDepartment("HR", 3000);
            await departmentService.AddDepartment("IT", 5000);
            await departmentService.AddDepartment("Finance", 4000);
        }

        public static async Task MigrateEmployee(EmployeeDepartmentService employeeService)
        {
            if ((await employeeService.GetList()).Any())
            {
                return;
            }
            await employeeService.AddEmployee(new DTOs.CreateEmployeeDto
            {
                EmployeeName = "Anas",
                DepartmentId = 1,
                BaseSalary = 500
            }); await employeeService.AddEmployee(new DTOs.CreateEmployeeDto
            {
                EmployeeName = "Omar",
                DepartmentId = 2,
                BaseSalary = 300
            }); await employeeService.AddEmployee(new DTOs.CreateEmployeeDto
            {
                EmployeeName = "Mohamad",
                DepartmentId = 1,
                BaseSalary = 900
            }); await employeeService.AddEmployee(new DTOs.CreateEmployeeDto
            {
                EmployeeName = "Yahya",
                DepartmentId = 2,
                BaseSalary = 400
            }); await employeeService.AddEmployee(new DTOs.CreateEmployeeDto
            {
                EmployeeName = "ahmad",
                DepartmentId = 3,
                BaseSalary = 500
            }); await employeeService.AddEmployee(new DTOs.CreateEmployeeDto
            {
                EmployeeName = "yazan",
                DepartmentId = 1,
                BaseSalary = 500
            }); await employeeService.AddEmployee(new DTOs.CreateEmployeeDto
            {
                EmployeeName = "anas",
                DepartmentId = 1,
                BaseSalary = 500
            });

            await employeeService.AddEmployee(new DTOs.CreateEmployeeDto
            {
                EmployeeName = "Anas",
                DepartmentId = 1,
                BaseSalary = 5000
            });
        }
    }
}
