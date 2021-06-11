//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace PixQrCodeGeneratorOffline.Models.Extentions
//{
//    public static class FeedExtention
//    {
//        public static string GetPublishDateDisplay(this Feed feed)
//        {
//            if (feed.PublishDuration.TotalSeconds < 60)
//                return "há " + (int)feed.PublishDuration.TotalSeconds + " segundos";

//            else if (feed.PublishDuration.TotalMinutes < 60)
//                return "há " + (int)feed.PublishDuration.TotalMinutes + " minutos";

//            else
//            {
//                if (!feed.PublishDateLocal.HasValue)
//                    return "";

//                else if (feed.IsToday)
//                    return "Hoje ás " + feed.PublishDateLocal.Value.ToString("HH tt");

//                else
//                    return "Ontem ás " + feed.PublishDateLocal.Value.ToString("HH tt");
//            }
//        }

//        public static string GetPublishDateDisplayFull(this Feed feed)
//        {
//            return feed.PublishDateLocal.HasValue ? feed.PublishDateLocal.Value.ToString("dd MMM yyyy HH:mm") : "";
//        }
//    }
//}
