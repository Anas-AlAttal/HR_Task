using HR_management_project.EventsArg;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.EventHandlers
{
    public class HolidaysEventHandler
    {
        public void HandleHolidayRequest(object sender, HolidayRequestedEventArg h)
        {
            Console.WriteLine($"Employee Id:{h.EmployeeID} request a holiday ({h.RequestedDays} days) whith state: {h.Status.ToString()}");
        }

    }
}
