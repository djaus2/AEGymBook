using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public static class Settings
    {
        public static bool ResetAuthDB { get; set; } = false;
        public static bool ResetDatahDB { get; set; } = false;
        public static bool HaveSetupAdmin { get; set; } = false;
        public const string AdminName = "AdminMe";
        public const string AdminPwd = "12345678";
        public const string MyName = "djaus";
        public static List<int> DaysOfTheWeek { get; set; } = new List<int> {0, 1, 2, 3, 4, 5, 6}; //Sun to Sat
        public static List<int> OpenDays { get; set; } = new List<int> {0, 1, 2, 3, 4, 5 }; //Sun  to Fri no Sat
        public static List<int> ClosedDays { get {
                return (DaysOfTheWeek.Except(OpenDays)).ToList<int>();
            } }
        public static bool FORDEVONLY_User_Can_book_more_than_once_at_one_time { get; set; } = false;
        public static TimeSpan AddBookingShowStartTime { get; set; } = new TimeSpan(16, 0, 0);
        public static int MaxNumberInRoom { get; set; } = 4;
        public static int TimeUnitMinutes { get; set; } = 30; //i.e. 1/2hr
        public static int MaxBookUnits { get; set; } = 2; //i.e. 1hr

        public static TimeSpan? OpenHourWeekDay { get; set; } = new TimeSpan(16, 0, 0); //4pm
        public static TimeSpan? CloseHourWeekDay { get; set; } = new TimeSpan(20, 0, 0);  //8pm

        public static TimeSpan? OpenHourSaturday { get; set; } = null;
        public static TimeSpan? CloseHourSaturday { get; set; } = null;

        public static TimeSpan? OpenHourSunday { get; set; } = new TimeSpan(9, 0, 0); //4pm
        public static TimeSpan? CloseHourSunday { get; set; } = new TimeSpan(12, 0, 0);  //8pm

        public static TimeSpan?[] OpenHours { get; set; }
        public static TimeSpan?[] CloseHours { get; set; }

        public  enum Days  {Sun,Mon,Tues,Wed,Thurs,Fri,Sat };
        public static void SetUpOpenCloseHrs()
        {
            OpenHours = new TimeSpan?[7];
            CloseHours = new TimeSpan?[7];


            for (Days day = Days.Mon; day < Days.Sat; day++)
            {
                OpenHours[(int)day] = OpenHourWeekDay;
                CloseHours[(int)day] = CloseHourWeekDay;
            }

            OpenHours[(int)Days.Sat] = OpenHourSaturday;
            CloseHours[(int)Days.Sat] = CloseHourSaturday;
            OpenHours[(int)Days.Sun] = OpenHourSunday;
            CloseHours[(int)Days.Sun] = CloseHourSunday;
        }

        public static TimeSpan ?WithCardOpenHour { get; set; } = new TimeSpan(9, 0, 0); //8am 12:00 for Sunday
        public static TimeSpan? WithCardCloseHour { get; set; } = null; //Open our - 30 mins for weekdays 20:00 for Sat and Sun; //8pm

    }
}
