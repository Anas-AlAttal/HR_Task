using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Interfaces
{
    public interface IHolidayStratigy
    {
        public int CalculateHolidays(int YearsOfService);
    }
}
