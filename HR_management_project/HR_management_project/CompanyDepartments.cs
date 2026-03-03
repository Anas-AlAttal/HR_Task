using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


namespace HR_management_project
{
    internal class CompanyDepartments
    {
        private readonly List<Department> Departments = new List<Department>();
        

        public void AddDepartment(Department department)
        {
            Departments.Add(department);
            //add leseners to all departments
            department.EmployeeAdded += (sender, e) => Notify.NotifyDepartmentEmployees(GetDepartmentById(e.Employee.DepartmentId), e.Employee,"added" );
            department.IncreaseDepartmentBalanceRequested += (sender, e) => Notify.NotifyCompanyeRequest(GetDepartmentById(e.Employee.DepartmentId),e.Employee.GetNetSalary());
            department.EmployeeRemoved += (sender, e) => Notify.NotifyDepartmentEmployees(GetDepartmentById(e.Employee.DepartmentId), e.Employee, "removed");
         
        }


        public Department GetDepartmentById(int departmentId)
        {
            return Departments.Find(d => d.DepartmentId == departmentId) ?? throw new ArgumentException("Department not found.");
        }

        
        public void PrintAllDepartments()
        {
            if (Departments.Count == 0)
            {
                Console.WriteLine("No departments available.");
                return;
            }
            Console.WriteLine("Departments:");
            foreach (var department in Departments)
            {
                Console.WriteLine($"ID: {department.DepartmentId}, Name: {department.DepartmentName}, Employee Count: {department.EmployeeCount}, Balance: {department.Balance}");
            }

        }

        public void MoveEmployee(int employeeId, int fromDepartmentId, int toDepartmentId)
        {
            var fromDepartment = GetDepartmentById(fromDepartmentId);
            var toDepartment = GetDepartmentById(toDepartmentId);

            var employee = fromDepartment.GetEmployees().FirstOrDefault(e => e.EmployeeId == employeeId) ?? throw new ArgumentException("Employee not found in the source department.");

            var newEmployee = new Employee(employee.EmployeeName, toDepartment.DepartmentId, employee.BaseSalary);
            newEmployee.SetDeduction(employee.Deduction);
            newEmployee.SetBonus(employee.Bonus);
            newEmployee.SetStartedDate(employee.DateOfJoining);

            fromDepartment.RemoveEmployee(employee);
            toDepartment.AddEmployee(newEmployee) 
            
            ;
        }
    }
}
