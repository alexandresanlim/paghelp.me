using System;
using System.Drawing;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions
{
    public static class FeedExtension
    {
        public static bool IsValid(this Feed feed)
        {
            return feed != null && !string.IsNullOrWhiteSpace(feed?.Title) && !string.IsNullOrEmpty(feed?.Link?.AbsoluteUri);
        }

        public static bool IsToday(this Feed feed)
        {
            return IsValid(feed) && feed.PublishDateLocal.HasValue && DateTimeOffset.Now.Date.Equals(feed.PublishDateLocal.Value.Date);
        }

        public static bool IsYesterday(this Feed feed)
        {
            return IsValid(feed) && feed.PublishDateLocal.HasValue && DateTimeOffset.Now.Date.AddDays(-1).Equals(feed.PublishDateLocal.Value.Date);
        }

        public static string GetPublishDateDisplay(this Feed feed)
        {
            if (!feed.IsValid())
                return "";

            if (feed.PublishDuration.TotalSeconds < 60)
                return "há " + (int)feed.PublishDuration.TotalSeconds + " segundos";

            else if (feed.PublishDuration.TotalMinutes < 60)
                return "há " + (int)feed.PublishDuration.TotalMinutes + " minutos";

            else
            {
                if (!feed.PublishDateLocal.HasValue)
                    return "";

                else if (feed.IsToday())
                    return "Hoje ás " + feed.PublishDateLocal.Value.ToString("HH tt");

                else if (feed.IsYesterday())
                    return "Ontem ás " + feed.PublishDateLocal.Value.ToString("HH tt");

                else
                    return feed.PublishDateLocal.Value.ToString("dd/MMM") + " " + feed.PublishDateLocal.Value.ToString("HH tt");
            }
        }

        public static string GetPublishDateDisplayFull(this Feed feed)
        {
            if (!feed.IsValid())
                return "";

            return feed.PublishDateLocal.HasValue ? feed.PublishDateLocal.Value.ToString("dd MMM yyyy HH:mm") : "";
        }

        public static FeedTag GetTag(this Feed feed)
        {
            var invalidFeedTag = new FeedTag
            {
                IsVisible = false
            };

            if (!feed.IsValid() || !feed.PublishDateLocal.HasValue)
                return invalidFeedTag;

            else if (feed.IsToday())
            {
                return new FeedTag
                {
                    Title = "Hoje",
                    Color = Color.Green,
                    IsVisible = true,
                };
            }

            else if (feed.IsYesterday())
            {
                return new FeedTag
                {
                    Title = "Ontem",
                    Color = Color.Blue,
                    IsVisible = true,
                };
            }

            return invalidFeedTag;
        }
    }
}
