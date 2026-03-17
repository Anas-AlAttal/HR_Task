using HR_management_project.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.DTOs
{
    public class HolidayRequestResult
    {
        public HolidayRequestStatus Status { get; set; }
        public string Massage { get; set; }
        public HolidayRequestResult(HolidayRequestStatus status, string massage)
        {
            Status = status;
            Massage = massage;
        }

        public static HolidayRequestResult Success(string massage)
        {
            return new HolidayRequestResult(HolidayRequestStatus.Approved, massage);
        }

        public static HolidayRequestResult Fail(string massage)
        {
            return new HolidayRequestResult(HolidayRequestStatus.Rejected, massage);
        }

        public static HolidayRequestResult InProccessed(string massage)
        {
            return new HolidayRequestResult(HolidayRequestStatus.Unapproved, massage);
        }

    }
}
