using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Extention
{
    public static class TimeExtention
    {
        public static bool IsBetween(this TimeSpan time, TimeSpan start, TimeSpan end)
        {
            // convert datetime to a TimeSpan
            var now = time; //datetime.TimeOfDay;
            // see if start comes before end
            if (start < end)
                return start <= now && now <= end;
            // start is after end, so do the inverse comparison
            return !(end < now && now < start);
        }
    }
}
