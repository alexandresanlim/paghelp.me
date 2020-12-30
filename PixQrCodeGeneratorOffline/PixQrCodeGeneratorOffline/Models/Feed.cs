using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models
{
    public class Feed
    {
        //public FeedItem()
        //{
        //    Topic = new Topic();
        //}

        public string Id { get; set; }

        public string Title { get; set; }

        public Uri Link { get; set; }

        public DateTimeOffset? PublishDate { get; set; }

        public DateTimeOffset? PublishDateLocal => PublishDate?.ToLocalTime();

        public string Description { get; set; }

        public string Source { get; set; }

        public string FirstImage { get; set; }

        //public Topic Topic { get; set; }

        public bool WasRead { get; set; }

        public int Index { get; set; }

        public bool IsToday => DateTimeOffset.Now.Date.Equals(PublishDateLocal.Value.Date);

        public TimeSpan PublishDuration => PublishDateLocal.HasValue ? (DateTimeOffset.Now - PublishDateLocal.Value) : TimeSpan.MaxValue;

        public string PublishDateDisplayFull => PublishDateLocal.HasValue ? PublishDateLocal.Value.ToString("dd MMM yyyy HH:mm") : "";

        public string PublishDateDisplay
        {
            get
            {
                if (PublishDuration.TotalSeconds < 60)
                    return "há " + (int)PublishDuration.TotalSeconds + " sec";

                else if (PublishDuration.TotalMinutes < 60)
                    return "há " + (int)PublishDuration.TotalMinutes + " min";

                else
                {
                    if (IsToday)
                        return "Today " + PublishDateLocal.Value.ToString("hh tt", CultureInfo.InvariantCulture);

                    else
                        return "Yesterday " + PublishDateLocal.Value.ToString("hh tt", CultureInfo.InvariantCulture);
                }
            }
        }
    }
}
