using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;
using System.Text.Json.Serialization;

namespace AthsEssGymBook.Shared
{
    public class BookingInfo
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("AthleteId")]
        public int AthleteId { get; set; }
        [JsonPropertyName("Slot")]
        public int Slot { get; set; } = 0;
        [JsonPropertyName("_Date")]
        public string _Date { get; set; } = DateTime.Now.ToString("yyyy-mm-dd");
        [JsonPropertyName("_Time")]
        public int _Time { get; set; } = 0; //In units of 30 minutes
        [JsonPropertyName("_Duration")]
        public int _Duration { get; set; } = 0; //In units of 30 minutes

        [NotMapped]
        [JsonIgnore]
        public DateTime Date
        {
            get { return GetDate(); }
            set   { SetDate(value); }
        }

        [NotMapped]
        [JsonIgnore]
        public TimeSpan Time
        {
            get { return GetTime(); }
            set { SetTime(value); }
        }

        [NotMapped]
        [JsonIgnore]
        public TimeSpan Duration
        {
            get { return GetDuration(); }
            set { SetDuration(value); }
        }

        public void SetDate (DateTime dat)
        {
            _Date = dat.ToString("yyyy-mm-dd");
        }
        public DateTime GetDate()
        {
            DateTime dat = DateTime.Now;
            if ( ! DateTime.TryParse(_Date, out dat)) { 
                dat = DateTime.Now;
            }
            return dat;
        }

        public void SetTime(TimeSpan dat)
        {
            int mins = (int)(dat.TotalMinutes / 30);
            _Time = mins; 
        }
        public TimeSpan GetTime()
        {
            TimeSpan time = new TimeSpan(0, _Time * 30, 0);
            return time;
        }
        public void SetDuration(TimeSpan dat)
        {
            int mins = (int)(dat.TotalMinutes / 30);
            _Duration = mins;
        }
        public TimeSpan GetDuration()
        {
            TimeSpan duration = new TimeSpan(0, _Duration * 30, 0);
            return duration;
        }
    }
    /*
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

    }*/
}
