using HR_management_project.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.EventHandlers
{
    public class EmployeeEventHandler
    {
        public void HandleEmployeeChanges(object sender, EmployeeEventArg e)
        {
            Console.WriteLine($"Employee {e.EmployeeName} with Id: {e.EmployeeID} was {e.Operation.ToString()}.");
        }
    }
}
