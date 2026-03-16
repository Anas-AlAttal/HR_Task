using HR_management_project.EventsArg;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.EventHandlers
{
    public class DepartmentBalanceEventHandler
    {

        public void HandleIncremantBalanceReaquest(object sender, DepartmentBalanceEventArg e)
        {
            Console.WriteLine($"Department {{Id: {e.Id}}} Request to increase balance from {e.Balance} to {e.NeededBalance}.");
        }
    }
}
