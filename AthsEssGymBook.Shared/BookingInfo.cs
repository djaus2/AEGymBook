using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;
//using System.Text.Json.Serialization;


namespace AthsEssGymBook.Shared
{
    public class BookingInfo
    {

        public int Id { get; set; }

        public int AthleteId { get; set; }


        public string _Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        public int _Time { get; set; } = 0; //In units of 30 minutes
 
        public int _Duration { get; set; } = 0; //In units of 30 minutes

        //[JsonPropertyName("Slots")]
        //public List<BookingSlot> Slots { get; set; } = new List<BookingSlot>()

        [NotMapped]
        [JsonIgnore]
        public virtual IList<int> Slots
        {
            get
            {
                return _Slots;
            }
            set
            {
                _Slots = value;
            }
        }

        private IList<int> _Slots = new List<int>();

        public string SlotsSerialized
        {
            get
            {
  
                return JsonConvert.SerializeObject(_Slots);
            }
            set
            {
                _Slots = JsonConvert.DeserializeObject<IList<int>>(value);
            }
        }

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
            _Date = dat.ToString("yyyy-MM-dd");
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

    public class BookingSlot
    {

        public int Id { get; set; }

        public string _Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        public int _Time { get; set; } = 0; //In units of 30 minutes

        [NotMapped]
        [JsonIgnore]
        public int Count { get; set; } = AthsEssGymBook.Shared.Settings.MaxNumberInRoom;

        [NotMapped]
        [JsonIgnore]
        public DateTime Date
        {
            get { return GetDate(); }
            set { SetDate(value); }
        }

        [NotMapped]
        [JsonIgnore]
        public TimeSpan Time
        {
            get { return GetTime(); }
            set { SetTime(value); }
        }


        public void SetDate(DateTime dat)
        {
            _Date = dat.ToString("yyyy-MM-dd");
        }
        public DateTime GetDate()
        {
            DateTime dat = DateTime.Now;
            if (!DateTime.TryParse(_Date, out dat))
            {
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

    }

}
