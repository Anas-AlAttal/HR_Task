namespace HR_management_project.EventsArg
{
    public class HolidayRequestedEventArg
    {
        public int EmployeeID { get; set; }
        public HolidayRequestedEventArg(int employeeID)
        {
            EmployeeID = employeeID;
        }



    }
}
