using System;
using System.Collections.Generic;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public class BookingInfo
    {
        public int Id { get; set; }
        public  UserInfo User { get; set; }
        public int Slot { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Duration { get; set; }

        public BookingInfo()
        {
        }

        public BookingInfo(BookingInfo_SQL bookingInfo)
        {
            User = bookingInfo.User;
            Slot = bookingInfo.Slot;
            Date = new DateTime(bookingInfo.Date);
            Time = new TimeSpan(bookingInfo.Time * 30);
            Duration = bookingInfo.Duration;
        }

    }

    public class BookingInfo_SQL
    {
        public UserInfo User { get; set; }
        public int Slot { get; set; }
        public long Date { get; set; }
        public int Time { get; set; }
        public int Duration { get; set; }

        public BookingInfo_SQL()
        {
        }
        public BookingInfo_SQL(BookingInfo bookingInfo)
        {
            User = bookingInfo.User;
            Slot = bookingInfo.Slot;
            Date = bookingInfo.Date.Ticks;
            Time = (int)(bookingInfo.Time.TotalMinutes / 30);
            Duration = bookingInfo.Duration;
        }

    }
}
