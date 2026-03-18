using HR_management_project.Data.Core;
using HR_management_project.Data.Stores.SqlLiteStore;
using HR_management_project.Data.Stores.StaticStore;
using HR_management_project.DTOs;
using HR_management_project.EventHandlers;
using HR_management_project.Factory;
using HR_management_project.Interfaces;
using HR_management_project.Migrator;
using HR_management_project.Model;
using HR_management_project.Services;
using HR_management_project.Stratigy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
internal class Program
{
    private static async Task Main(string[] args)
    {
        var servicers = new ServiceCollection();
        
        servicers.AddSingleton<ApplicationDbContext>();
        
        servicers.AddScoped<IDataStore>(sp =>
        {
            var context = sp.GetRequiredService<ApplicationDbContext>();
            return  DataStoreFactory.CreateDataStore(context);
        });

        servicers.AddScoped<IHolidayStrategy, HolidayDefultStratigy>();
        servicers.AddScoped<HolidaysEventHandler>();
        servicers.AddScoped<EmployeeEventHandler>();
        servicers.AddScoped<DepartmentBalanceEventHandler>();

        servicers.AddScoped<HolidaysService>();
        servicers.AddScoped<EmployeeService>();
        servicers.AddScoped<DepartmentService>();
        servicers.AddScoped<EmployeeDepartmentService>();

        var provider = servicers.BuildServiceProvider();

        var departmentService = provider.GetService<DepartmentService>();
        var employeeService = provider.GetService<EmployeeService>();
        var employeeDepartmentService = provider.GetService<EmployeeDepartmentService>();
        var holidayService = provider.GetService<HolidaysService>();


        await StaticDataSeder.MigrateDepartments(departmentService);
        await StaticDataSeder.MigrateEmployee(employeeDepartmentService);

        await holidayService.RequestHoliday(3, 100);
















        //HolidaysEventHandler holidaysEventHandler = new HolidaysEventHandler();
        //EmployeeEventHandler employeeEventHandler = new EmployeeEventHandler();
        //DepartmentBalanceEventHandler departmentBalanceEventHandler = new DepartmentBalanceEventHandler();

        //ApplicationDbContext context = new ApplicationDbContext();

        //// using Dependency Injection
        //IDataStore dataStor = DataStoreFactory.CreateDataStore(context);

        //DepartmentService departmentService = new(dataStor);
        //EmployeeService employeeService = new(dataStor, employeeEventHandler);
        //EmployeeDepartmentService employeeDepartmentService = new(dataStor, employeeEventHandler, departmentBalanceEventHandler);
        //HolidaysService HolidayService = new HolidaysService(new HolidayDefultStratigy(), dataStor, holidaysEventHandler);

        //await StaticDataSeder.MigrateDepartments(departmentService);
        //Console.WriteLine("---------------------------------------------------");

        //await StaticDataSeder.MigrateEmployee(employeeDepartmentService);
        //Console.WriteLine("---------------------------------------------------");

        ////await employeeDepartmentService.MoveEmployee(new MoveEmployeeDto
        ////{
        ////    EmployeeId = 3,
        ////    FromDepartmentId = 2,
        ////    ToDepartmentId = 1
        ////});

        ////----------------------------------------------------------------------------------------
        ////moving emloyee from hr to finance
        ////departmentService.MoveEmployee(1, hrDepartment.Id, financeDepartment.Id);

        ////var emp = hrDepartment.GetEmployeeById(2);

        //await HolidayService.RequestHoliday(2, 1);
        //Console.WriteLine("-----------------------------------");
        //Console.WriteLine((await dataStor.GetData<Employee, int>(2)).ToString());

        ////emp.SetStartedDate(new DateTime(2020, 1, 1));
        ////Console.WriteLine(emp.YearsOfService);
        //Console.WriteLine($"Total holidays = {await HolidayService.GetEmployeeHolidays(3)}");

        //Console.WriteLine(await employeeDepartmentService.TotalEmployeesBalanceInDepartment(3));
    }


}