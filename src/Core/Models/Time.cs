﻿namespace Core.Models
{
    public class Time
    {
        public int Id { get; set; }
        public string TimeValue { get; set; }
        public ICollection<AppointmentTime> AppointmentTimes { get; set; }
    }
}
