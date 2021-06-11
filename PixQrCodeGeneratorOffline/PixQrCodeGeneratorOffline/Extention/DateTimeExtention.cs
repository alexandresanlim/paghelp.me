//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace PixQrCodeGeneratorOffline.Extention
//{
//    public class DateTimeExtention
//    {
//        public static string GetAGreatingToCurrentDate()
//        {
//            var currentDateTime = DateTime.Now;

//            var period = currentDateTime.Hour;

//            var msg = "";

//            if (period > 12 && period < 18)
//                msg += "Boa tarde e ";

//            else if (period >= 18)
//                msg += "Boa noite e ";

//            else
//                msg += "Bom dia e ";

//            var dayOfWeed = currentDateTime.DayOfWeek;

//            switch (dayOfWeed)
//            {
//                case DayOfWeek.Friday:
//                    msg += "#Sextou! ótimo fim de semana.";
//                    break;

//                case DayOfWeek.Saturday:
//                    msg += "ótimo fim de semana";
//                    break;
//                case DayOfWeek.Sunday:
//                    msg += "ótimo domingo";
//                    break;

//                case DayOfWeek.Monday:
//                case DayOfWeek.Tuesday:
//                    msg += "ótimo começo de semana";
//                    break;

//                case DayOfWeek.Thursday:
//                    msg += "excelente quinta-feira";
//                    break;
//                case DayOfWeek.Wednesday:
//                    msg += "boa quarta-feira";
//                    break;
//            }

//            msg += ".";

//            return msg;
//        }

//        //public static string GetHolidayToCurrentDate()
//        //{
//        //    var holiday = Holidays.NationalHolidays.From("br").OfYear(DateTime.Today.Year).FirstOrDefault(x => x.Value.Date.Equals(DateTime.Today)).Key;

//        //    if (string.IsNullOrEmpty(holiday))
//        //        return "";

//        //    return "Bom Feriado! " + holiday;
//        //}

//        //public static bool TodayIsHoliday()
//        //{
//        //    return !string.IsNullOrEmpty(GetHolidayToCurrentDate());
//        //}
//    }
//}
