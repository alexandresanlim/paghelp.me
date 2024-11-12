using BrazilHolidays.Net;
using System;

namespace PixQrCodeGeneratorOffline.Extention
{
    public class DateTimeExtention
    {
        public static string GetDashboardTitleFromPeriod()
        {
            var period = DateTime.Now.Hour;

            var msg = "Olá, ";

            if (period > 4 && period < 12)
                msg += "Bom dia";

            else if (period >= 12 && period < 18)
                msg += "Boa tarde";

            else
                msg += "Boa noite";

            msg += "!";

            return msg;
        }

        public static string GetDashboardSubtitleFromDayOfWeed()
        {
            var currentDateTime = DateTime.Now;

            var isHoliday = DateTime.Today.IsHoliday();

            var msg = "";

            if (isHoliday)
            { 
                msg += "e bom feriado.";
                return msg;
            }

            switch (currentDateTime.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    msg += "e ótimo fim de semana.";
                    break;

                case DayOfWeek.Saturday:
                    msg += "e ótimo sábado";
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
                    msg += "e excelente quinta-feira";
                    break;

                case DayOfWeek.Wednesday:
                    msg += "e boa quarta-feira";
                    break;
            }

            msg += ".";

            return msg;
        }
    }
}
