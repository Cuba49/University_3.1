using System;

namespace TimeService
{
    public class TimeService : ITimeService
    {
        public string GetCurrentTime()
        {
            return DateTime.Now.ToShortTimeString();
        }
    }
}