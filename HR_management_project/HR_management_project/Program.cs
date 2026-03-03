using HR_management_project;
internal class Program
{
    private static void Main(string[] args)
    {
      CompanyDepartments companyDepartments = new CompanyDepartments();

        //Create departments
        var hrDepartment = new Department("HR", 3000);
        var itDepartment = new Department("IT", 5000);
        var financeDepartment = new Department("Finance", 4000);


        // Adding departments to the company
        companyDepartments.AddDepartment(hrDepartment);
        companyDepartments.AddDepartment(itDepartment);
        companyDepartments.AddDepartment(financeDepartment);

        // Adding Emloyees
        hrDepartment.AddEmployee(new Employee("Ahmad", hrDepartment.DepartmentId, 500));
        Console.WriteLine("-----------------------------------");
        
        hrDepartment.AddEmployee(new Employee("Anas", hrDepartment.DepartmentId, 500));
        Console.WriteLine("-----------------------------------");

        hrDepartment.AddEmployee(new Employee("Mohamad", hrDepartment.DepartmentId, 500));
        Console.WriteLine("-----------------------------------");
        hrDepartment.AddEmployee(new Employee("Omar", hrDepartment.DepartmentId, 200));

        Console.WriteLine("-----------------------------------");
        itDepartment.AddEmployee(new Employee("Ahmad", itDepartment.DepartmentId, 500));
        Console.WriteLine("-----------------------------------");

        itDepartment.AddEmployee(new Employee("Anas", itDepartment.DepartmentId, 500));
        Console.WriteLine("-----------------------------------");

        itDepartment.AddEmployee(new Employee("Mohamad", itDepartment.DepartmentId, 500));
        Console.WriteLine("-----------------------------------");

        itDepartment.AddEmployee(new Employee("Omar", itDepartment.DepartmentId, 200));
        Console.WriteLine("-----------------------------------");

        //moving emloyee from hr to finance
        companyDepartments.MoveEmployee(1, hrDepartment.DepartmentId, financeDepartment.DepartmentId);

        var emp = hrDepartment.GetEmployeeById(2);
        emp.RequestHoliday();

        Console.WriteLine("-----------------------------------");
        companyDepartments.PrintAllDepartments();

    }


}