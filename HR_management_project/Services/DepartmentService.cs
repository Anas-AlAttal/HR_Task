using HR_management_project.Data.Core;
using HR_management_project.DTOs;
using HR_management_project.Event;
using HR_management_project.Model;
using System.Threading.Tasks;

namespace HR_management_project.Services
{
    public class DepartmentService
    {
        private readonly IDataStore _dataStore;
        public event EventHandler<EmployeeEventArg> IncreaseDepartmentBalanceRequested;

        public DepartmentService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }


        public async Task<Department> AddDepartment(string debartmentName, decimal balance)
        {
            var department = new Department(debartmentName, balance);
            return await _dataStore.Add(department);
        }
        public async Task<bool> IsExistedDepartment(int departmentId)
        {
            try
            {
                var department = await _dataStore.GetData<Department, int>(departmentId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<Department>> GetList()
        {
            try
            {
                var departments = await _dataStore.GetList<Department>();
                return departments;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Department> GetDepartmentById(int departmentId)
        {
            return await _dataStore.GetData<Department, int>(departmentId);
        }
        public async Task PrintAllDepartments()
        {
            var dep = await _dataStore.GetList<Department>();

            if (dep == null)
            {
                Console.WriteLine("No departments available.");
                return;
            }
            Console.WriteLine("Departments:");
            foreach (var department in dep)
            {
                Console.WriteLine($"ID: {department.Id}, Name: {department.DepartmentName}, Balance: {department.Balance}");//, Employee Count: {department.EmployeeCount}");
            }

        }
        public async Task<int> GetEmployeeCountInDepartment(int departmentId)
        {
            var empList = await _dataStore.GetList<Employee>(e => e.DepartmentId == departmentId);
            return empList.Count;
        }




    }
}
