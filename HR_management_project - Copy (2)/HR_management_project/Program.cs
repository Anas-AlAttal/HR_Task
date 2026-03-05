using HR_management_project;
using HR_management_project.Data;
using HR_management_project.Migrator;
using HR_management_project.Services;
internal class Program
{
    private static async Task Main(string[] args)
    {
        var departmentService = new DepartmentService(new StaticDataStore());
        var employeeService = new EmployeeService(new StaticDataStore());

        await DataMigrator.MigrateDepartments(departmentService);
        await DataMigrator.MigrateEmployees(employeeService);

        var departments = await departmentService.GetDepartments();
        var employees = await employeeService.GetEmployees();

        foreach (var dept in departments)
        {
            Console.WriteLine(dept.DepartmentName);
        }
        foreach (var emp in employees)
        {
            Console.WriteLine(emp.EmployeeName);
            Console.WriteLine(emp.DepartmentId);
        }
    }


}