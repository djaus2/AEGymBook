using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public static class Settings
    {
        public const string AdminName = "AdminMe";
        public static List<int> DaysOfTheWeek { get; set; } = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        public static List<int> OpenDays { get; set; } = new List<int> { 1, 2, 3, 4, 5, 7 }; //Mon to Sun no Sat
        public static List<int> ClosedDays { get {
                return (DaysOfTheWeek.Except(OpenDays)).ToList<int>();
            } }
        public static bool FORDEVONLY_User_Can_book_more_than_once_at_one_time { get; set; } = false;
        public static TimeSpan AddBookingShowStartTime { get; set; } = new TimeSpan(16, 0, 0);
        public static int MaxNumberInRoom { get; set; } = 4;
        public static int TimeUnitMinutes { get; set; } = 30; //i.e. 1/2hr
        public static int MaxBookUnits { get; set; } = 2; //i.e. 1hr

        public static TimeSpan OpenHour { get; set; } = new TimeSpan(16, 0, 0); //4pm
        public static TimeSpan CloseHour { get;  set; } = new TimeSpan(17,30,0);  //8pm

        public static TimeSpan WithCardOpenHour { get; set; } = new TimeSpan(9, 0, 0); //8am
        public static TimeSpan WithCardCloseHour { get; set; } = CloseHour; //8pm
    }
}
