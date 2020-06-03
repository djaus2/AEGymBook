using System;
using System.Collections.Generic;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public static class Settings
    {
        public static int MaxNumberInRoom { get; set; } = 4;
        public static int TimeUnitMinutes { get; set; } = 30; //i.e. 1/2hr
        public static int MaxBookUnits { get; set; } = 2; //i.e. 1hr

        public static TimeSpan OpenHour { get; set; } = new TimeSpan(16, 0, 0); //4pm
        public static TimeSpan CloseHour { get;  set; } = new TimeSpan(20,0,0);  //8pm

        public static TimeSpan WithCardOpenHour { get; set; } = new TimeSpan(9, 0, 0); //8am
        public static TimeSpan WithCardCloseHour { get; set; } = CloseHour; //8pm
    }
}
