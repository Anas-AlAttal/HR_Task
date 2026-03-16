using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.EventsArg
{
    public class DepartmentBalanceEventArg
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal NeededBalance { get; set; }
        public DepartmentBalanceEventArg(int id, decimal balance, decimal neededBalance)
        {
            Id = id;
            Balance = balance;
            NeededBalance = neededBalance;
        }

       

    }
}
