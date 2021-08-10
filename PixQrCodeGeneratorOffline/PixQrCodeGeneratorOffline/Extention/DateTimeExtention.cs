using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixQrCodeGeneratorOffline.Extention
{
    public class DateTimeExtention
    {
        public static string GetDashboardTitleFromPeriod()
        {
            var currentDateTime = DateTime.Now;

            var period = currentDateTime.Hour;

            var msg = "Olá, ";

            if (period > 12 && period < 18)
                msg += "Boa tarde";

            else if (period >= 18)
                msg += "Boa noite";

            else
                msg += "Bom dia";

            msg += "!";

            return msg;
        }

        public static string GetDashboardSubtitleFromDayOfWeed()
        {
            var currentDateTime = DateTime.Now;

            var msg = "";

            var dayOfWeed = currentDateTime.DayOfWeek;

            switch (dayOfWeed)
            {
                case DayOfWeek.Friday:
                    msg += "e ótimo fim de semana.";
                    break;

                case DayOfWeek.Saturday:
                    msg += "e ótimo fim de semana";
                    break;
                case DayOfWeek.Sunday:
                    msg += "e ótimo domingo";
                    break;

                case DayOfWeek.Monday:
                    msg += "e ótimo começo de semana";
                    break;

                case DayOfWeek.Tuesday:
                    msg += "e ótima terça-feira";
                    break;

                case DayOfWeek.Thursday:
                    msg += "excelente quinta-feira";
                    break;
                case DayOfWeek.Wednesday:
                    msg += "boa quarta-feira";
                    break;
            }

            msg += ".";

            return msg;
        }

        //public static string GetHolidayToCurrentDate()
        //{
        //    var holiday = Holidays.NationalHolidays.From("br").OfYear(DateTime.Today.Year).FirstOrDefault(x => x.Value.Date.Equals(DateTime.Today)).Key;

        //    if (string.IsNullOrEmpty(holiday))
        //        return "";

        //    return "Bom Feriado! " + holiday;
        //}

        //public static bool TodayIsHoliday()
        //{
        //    return !string.IsNullOrEmpty(GetHolidayToCurrentDate());
        //}
    }
}
