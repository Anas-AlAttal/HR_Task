using HR_management_project.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Services
{
    public class EmployeeService
    {
        private readonly IDataStore _dataStore;

        public EmployeeService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<Employee> AddEmployee(string employeeName, int departmentId, decimal baseSalary)
        {
            var employee = new Employee(employeeName, departmentId, baseSalary);
            var departments = await _dataStore.GetList<Department>();
            var department = departments.Find(d => d.DepartmentId == departmentId);
            if (department == null)
                throw new ArgumentException($"Department with ID {departmentId} does not exist.");
            department.AddEmployee(employee);
            await _dataStore.Add<Employee>(employee);
            return employee;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            return await _dataStore.GetList<Employee>();
        }
        public async Task DeleteEmployee(Employee employee)
        {
            var departments = await _dataStore.GetList<Department>();
            var department = departments.Find(d => d.DepartmentId == employee.DepartmentId);
            if (department != null)
            {
                department.RemoveEmployee(employee);
            }
            await _dataStore.Delete<Employee>(employee);
        }
    }
    public class DepartmentService
    {
        private readonly IDataStore _dataStore;

        public DepartmentService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<Department> AddDepartment(string departmentName, decimal balance)
        {
            var department = new Department(departmentName, balance);


            return await _dataStore.Add<Department>(department);
        }
        public async Task<List<Department>> GetDepartments()
        {
            return await _dataStore.GetList<Department>();
        }
        public async Task DeleteDepartment(Department department)
        {
            await _dataStore.Delete<Department>(department);
        }
    }
}
