using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Interfaces
{
    public interface IHolidayStrategy
    {
        public int CalculateHolidays(int YearsOfService);
    }
}
