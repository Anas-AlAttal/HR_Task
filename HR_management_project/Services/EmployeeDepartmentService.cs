using HR_management_project.Data.Core;
using HR_management_project.DTOs;
using HR_management_project.Enums;
using HR_management_project.Event;
using HR_management_project.EventHandlers;
using HR_management_project.EventsArg;
using HR_management_project.Model;

namespace HR_management_project.Services
{
    public class EmployeeDepartmentService
    {
        private readonly IDataStore _dataStore;
        private readonly EmployeeEventHandler _employeeEventHandler;
        private readonly DepartmentBalanceEventHandler _departmentBalanceEventHandler;

        public event EventHandler<EmployeeEventArg> EmployeeState;
        public event EventHandler<DepartmentBalanceEventArg> DepartmentBalanceRequest;
        public EmployeeDepartmentService(IDataStore dataStore, EmployeeEventHandler employeeEventHandler, DepartmentBalanceEventHandler departmentBalanceEventHandler)
        {
            _dataStore = dataStore;
            _employeeEventHandler = employeeEventHandler;
            _departmentBalanceEventHandler = departmentBalanceEventHandler;

            EmployeeState += _employeeEventHandler.HandleEmployeeChanges;
            DepartmentBalanceRequest += _departmentBalanceEventHandler.HandleIncremantBalanceReaquest;
        }
        public async Task AddEmployee(CreateEmployeeDto dto)
        {     
            var existedEmployee = await _dataStore.GetData<Employee, int>(dto.EmployeeId);

            if (existedEmployee != null)
                throw new Exception("Epmloyee is existed");

            if (await IsEnoughDepartmentBalance(dto.DepartmentId, dto.BaseSalary))
            {
                var emp = new Employee(dto.EmployeeName, dto.DepartmentId, dto.BaseSalary);
                await _dataStore.Add(emp);
                EmployeeState?.Invoke(this, new EmployeeEventArg(emp.Id, emp.EmployeeName, EmployeeOperation.Added));
            }
            else
            {
                var department = await _dataStore.GetData<Department, int>(dto.DepartmentId);
                DepartmentBalanceRequest?.Invoke(this, new DepartmentBalanceEventArg(department.Id, department.Balance, department.Balance + dto.BaseSalary));
            }
        }
        public async Task MoveEmployee(MoveEmployeeDto dto)
        {
            var employee = await _dataStore.GetData<Employee, int>(dto.EmployeeId);
            if (employee == null)
                throw new Exception("Employee not found");
                
            if (employee.DepartmentId != dto.FromDepartmentId)
                throw new Exception("Employee not in this dipartment.");

            if (await IsEnoughDepartmentBalance(dto.ToDepartmentId, employee.GetNetSalary()))
            {
                employee.ChangeDepartment(dto.ToDepartmentId);
                await _dataStore.Update<Employee, int>(employee);
                EmployeeState?.Invoke(this, new EmployeeEventArg(employee.Id, employee.EmployeeName, EmployeeOperation.Moved));
            }
            else
            {
                var department = await _dataStore.GetData<Department, int>(dto.ToDepartmentId);
                DepartmentBalanceRequest?.Invoke(this, new DepartmentBalanceEventArg(department.Id, department.Balance, department.Balance + employee.GetNetSalary()));
            }
        }
        public async Task RemoveEmployee(int employeeId, int departmentId)
        {
            var employee = await _dataStore.GetData<Employee, int>(employeeId);
            if (employee.DepartmentId != departmentId)
                throw new Exception("Employee not in this dipartment.");
            await _dataStore.Delete<Employee>(employee);
            EmployeeState?.Invoke(this, new EmployeeEventArg(employee.Id, employee.EmployeeName, EmployeeOperation.Removed));
        }
        public async Task<int> GetDepartmensEmployeeCount(int departmentId)
        {
            var list = await _dataStore.GetList<Employee>(e => e.DepartmentId == departmentId);
            return list.Count;
        }
        public async Task<bool> IsEnoughDepartmentBalance(int departmentId, decimal empBalance)
        {
            var dep = await _dataStore.GetData<Department, int>(departmentId);
            decimal totalEmployeesSalary = empBalance + await TotalEmployeesBalanceInDepartment(departmentId);
            if (dep.Balance >= totalEmployeesSalary)
                return true;

            return false;
        }
        public async Task<decimal> TotalEmployeesBalanceInDepartment(int departmentId)
        {
            var empList = await _dataStore.GetList<Employee>(e => e.DepartmentId == departmentId);
            return empList.Sum(e => e.GetNetSalary());
        }
        public async Task<List<Employee>> GetList()
        {
                var employees = await _dataStore.GetList<Employee>();
                return employees;
        }
    }
}
