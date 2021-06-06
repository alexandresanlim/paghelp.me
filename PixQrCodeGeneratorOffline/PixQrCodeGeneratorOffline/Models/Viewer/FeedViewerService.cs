using PixQrCodeGeneratorOffline.Models.Viewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Viewer
{
    public class FeedViewerService : IFeedViewerService
    {
        public string GetPublishDateDisplay(Feed feed)
        {
            if (feed.PublishDuration.TotalSeconds < 60)
                return "há " + (int)feed.PublishDuration.TotalSeconds + " segundos";

            else if (feed.PublishDuration.TotalMinutes < 60)
                return "há " + (int)feed.PublishDuration.TotalMinutes + " minutos";

            else
            {
                if (!feed.PublishDateLocal.HasValue)
                    return "";

                else if (feed.IsToday)
                    return "Hoje ás " + feed.PublishDateLocal.Value.ToString("HH tt");

                else
                    return "Ontem ás " + feed.PublishDateLocal.Value.ToString("HH tt");
            }
        }

        public string GetPublishDateDisplayFull(Feed feed)
        {
            return feed.PublishDateLocal.HasValue ? feed.PublishDateLocal.Value.ToString("dd MMM yyyy HH:mm") : "";
        }
    }
}
