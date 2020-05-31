using System;
using System.Collections.Generic;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public class BookingInfo
    {
        public UserInfo Athlete { get; set; }
        public int Slot { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan Duration { get; set; }

    }
}
