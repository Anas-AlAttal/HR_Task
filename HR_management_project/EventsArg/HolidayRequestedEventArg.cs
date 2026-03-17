using HR_management_project.Enums;

namespace HR_management_project.EventsArg
{
    public class HolidayRequestedEventArg
    {
        public int EmployeeID { get; set; }
        public int RequestedDays { get; }
        public int RemainingDays { get; }
        public HolidayRequestStatus Status { get; set; }

        public HolidayRequestedEventArg(int employeeID,int requestedDays, int remainingDays, HolidayRequestStatus status)
        {
            EmployeeID = employeeID;
            RequestedDays = requestedDays;
            RemainingDays = remainingDays;
            Status = status;
        }



    }
}
