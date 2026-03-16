using HR_management_project.Data;
using HR_management_project.DTOs;
using HR_management_project.Enums;
using HR_management_project.Event;
using HR_management_project.EventHandlers;
using HR_management_project.Model;

namespace HR_management_project.Services
{
    internal class EmployeeService
    {
        private readonly IDataStore _dataStore;

        private readonly EmployeeEventHandler _employeeEventHandler;
        public event EventHandler<EmployeeEventArg> EmployeeState;

        public EmployeeService(IDataStore dataStore, EmployeeEventHandler employeeEventHandler)
        {
            _dataStore = dataStore;
            _employeeEventHandler = employeeEventHandler;
            EmployeeState += _employeeEventHandler.HandleEmployeeChanges;
        }

        //public async Task<Employee> CreateEmployee(CreateEmployeeDto dto)
        //{
        //    if (await _dataStore.GetData<Employee, int>(dto.EmployeeId) != null)
        //        throw new Exception("Epmloyee is existed");
        //    return new Employee(dto.EmployeeName, dto.DepartmentId, dto.BaseSalary);
        //}

      
        public async Task<List<Employee>> GetList()
        {
            try
            {
                var employees = await _dataStore.GetList<Employee>();
                return employees;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> IsExistedEmployee(int employeeId, Department department)
        {
            var emp = await _dataStore.GetData<Employee, int>(employeeId);
            if (emp == null && department.Id == employeeId)
                return true;
            return false;
        }

    }
}
