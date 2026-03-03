namespace HR_management_project
{
    public class EmployeeEventArgs : EventArgs
    {
       public  Employee Employee { get; }

        public EmployeeEventArgs(Employee employee)
        {
            Employee = employee;
            
        }
        

    }
}