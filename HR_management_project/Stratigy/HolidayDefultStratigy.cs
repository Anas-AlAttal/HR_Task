using System;
using System.Collections.Generic;
using System.Text;
using HR_management_project.Interfaces;

namespace HR_management_project.Stratigy
{
    public class HolidayDefultStratigy : IHolidayStratigy
    {
        

        public int CalculateHolidays(int YearsOfService)
        {
            int totalHolidays = YearsOfService > 5 ? 21 : 14;
            return totalHolidays;
        }
    }
}