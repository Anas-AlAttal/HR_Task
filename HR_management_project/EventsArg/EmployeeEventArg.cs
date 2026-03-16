using HR_management_project.Enums;
using HR_management_project.Model;

namespace HR_management_project.Event
{
    public class EmployeeEventArg : EventArgs
    {
      public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public EmployeeOperation Operation { get; set; }
        public EmployeeEventArg(int employeeID, string employeeName, EmployeeOperation operation)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            Operation = operation;
        }
    }
}