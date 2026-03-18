using HR_management_project.Data.Core;
using HR_management_project.DTOs;
using HR_management_project.Enums;
using HR_management_project.EventHandlers;
using HR_management_project.EventsArg;
using HR_management_project.Interfaces;
using HR_management_project.Model;

namespace HR_management_project.Services
{
    public class HolidaysService
    {


        private readonly IHolidayStrategy _holidayStrategy;
        private readonly IDataStore _dataStore;
        private readonly HolidaysEventHandler _holidaysEventHandler;
        private readonly Func<int, int>? _customeCalculator;

        public event EventHandler<HolidayRequestedEventArg> HolidayRequest;

        public HolidaysService(
                IHolidayStrategy holidayStratigy,
                IDataStore dataStore,
                HolidaysEventHandler holidaysEventHandler,
                Func<int, int>? customeCalculator = null)
        {
            _holidayStrategy = holidayStratigy;
            _dataStore = dataStore;
            _holidaysEventHandler = holidaysEventHandler;
            _customeCalculator = customeCalculator;
            HolidayRequest += _holidaysEventHandler.HandleHolidayRequest;
        }


        public async Task<int> GetEmployeeHolidays(int employeeId)
        {
            var emp = await _dataStore.GetData<Employee, int>(employeeId);

            if (emp == null)
            {
                throw new Exception("Employee not found.");
            }
            if (_customeCalculator != null)
                _customeCalculator(emp.YearsOfService);

            return _holidayStrategy.CalculateHolidays(emp.YearsOfService);
        }



        public async Task<HolidayRequestResult> RequestHoliday(int empId, int requestedDays)
        {
            var emp = await _dataStore.GetData<Employee, int>(empId);

            if (emp == null)
                throw new Exception("Employee not found");

            int allowedHolidays = await GetEmployeeHolidays(emp.Id);
            int remainingDays = requestedDays - allowedHolidays;

            if (requestedDays < 0)
                return HolidayRequestResult.Fail("Invalid number of days");

            int takenDays = emp.TakenHolidays;

            if (takenDays + requestedDays > allowedHolidays)
            {
               HolidayRequest?.Invoke(this, new HolidayRequestedEventArg(empId, requestedDays, remainingDays, HolidayRequestStatus.Rejected));
                return HolidayRequestResult.Fail("Not enough holiday balance.");
            }

            emp.TakeHoliday(requestedDays);
            await _dataStore.Update<Employee, int>(emp);

            HolidayRequest?.Invoke(this, new HolidayRequestedEventArg(empId, requestedDays, remainingDays, HolidayRequestStatus.Unapproved));
            return HolidayRequestResult.InProccessed("Wating fore Aproved.");

        }
    }
}
