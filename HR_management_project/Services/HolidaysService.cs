using HR_management_project.Data;
using HR_management_project.Event;
using HR_management_project.EventHandlers;
using HR_management_project.EventsArg;
using HR_management_project.Interfaces;
using HR_management_project.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR_management_project.Services
{
    public class HolidaysService
    {
        

        private readonly IHolidayStratigy _holidayStratigy;
        private readonly IDataStore _dataStore;
        private readonly HolidaysEventHandler _holidaysEventHandler;

       
        public event EventHandler<HolidayRequestedEventArg> HolidayRequest;

        public HolidaysService(IHolidayStratigy holidayStratigy , IDataStore dataStore, HolidaysEventHandler holidaysEventHandler)
        {
            _holidayStratigy = holidayStratigy;
            _dataStore = dataStore;
            _holidaysEventHandler = holidaysEventHandler;
            HolidayRequest += _holidaysEventHandler.HandleHolidayRequest;
        }

        //extetion method to calculate holidays based on years of service
        // make it more flixible with a delegete to calculate holidays based on years of service
        public async Task<int> GetEmployeeHolidays(int employeeId)
        {
          var emp =  await _dataStore.GetData<Employee, int>(employeeId);
            return  _holidayStratigy.CalculateHolidays(emp.YearsOfService);
        }
//do again
        public void RequestHoliday(int empId)
        {
            HolidayRequest?.Invoke(this,new HolidayRequestedEventArg ( empId ));
        }
    }
}
