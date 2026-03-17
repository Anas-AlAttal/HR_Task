using HR_management_project.Data.Core;
using HR_management_project.Data.Stores.SqlLiteStore;
using HR_management_project.Data.Stores.StaticStore;
using HR_management_project.DTOs;
using HR_management_project.EventHandlers;
using HR_management_project.Migrator;
using HR_management_project.Model;
using HR_management_project.Services;
using HR_management_project.Stratigy;
internal class Program
{
    private static async Task Main(string[] args)
    {
        HolidaysEventHandler holidaysEventHandler = new HolidaysEventHandler();
        EmployeeEventHandler employeeEventHandler = new EmployeeEventHandler();
        DepartmentBalanceEventHandler departmentBalanceEventHandler = new DepartmentBalanceEventHandler();


        ApplicationDbContext context = new ApplicationDbContext();

        IDataStore dataStor = new DataBaseStore(context);
        //IDataStore dataStor = new StaticDataStore();


        //IDataStoreShow dataStorShow = new DataBaseStore(context);
        //IDataStoreShow dataStorShow = new StaticDataStore();

        DepartmentService departmentService = new(dataStor);
        EmployeeService employeeService = new(dataStor, employeeEventHandler);
        EmployeeDepartmentService employeeDepartmentService = new(dataStor, employeeEventHandler, departmentBalanceEventHandler);
        HolidaysService HolidayService = new HolidaysService(new HolidayDefultStratigy(), dataStor, holidaysEventHandler);

        await StaticDataSeder.MigrateDepartments(departmentService);
        Console.WriteLine("---------------------------------------------------");

        await StaticDataSeder.MigrateEmployee(employeeDepartmentService);
        Console.WriteLine("---------------------------------------------------");

        //await employeeDepartmentService.MoveEmployee(new MoveEmployeeDto
        //{
        //    EmployeeId = 3,
        //    FromDepartmentId = 2,
        //    ToDepartmentId = 1
        //});

        //dataStor.PrintAllData();
        Console.WriteLine("---------------------------------------------------");
      
        //await employeeDepartmentService.RemoveEmployee(1, 2);
       
        Console.WriteLine("---------------------------------------------------");
        //dataStor.PrintAllData();







        //----------------------------------------------------------------------------------------
        //moving emloyee from hr to finance
        //departmentService.MoveEmployee(1, hrDepartment.Id, financeDepartment.Id);

        //var emp = hrDepartment.GetEmployeeById(2);

        await HolidayService.RequestHoliday(2,1);
        Console.WriteLine("-----------------------------------");
        Console.WriteLine((await dataStor.GetData<Employee,int>(2)).ToString() );

        //emp.SetStartedDate(new DateTime(2020, 1, 1));
        //Console.WriteLine(emp.YearsOfService);
        Console.WriteLine($"Total holidays = { await HolidayService.GetEmployeeHolidays(3)}");

        Console.WriteLine(await employeeDepartmentService.TotalEmployeesBalanceInDepartment(3));
    }


}