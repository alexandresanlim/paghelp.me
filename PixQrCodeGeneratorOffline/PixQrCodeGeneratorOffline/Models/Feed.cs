using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class Feed
    {
        //public FeedItem()
        //{
        //    Topic = new Topic();
        //}

        private readonly IFeedViewer _feedViewer;

        public Feed()
        {
            _feedViewer = DependencyService.Get<IFeedViewer>();
        }

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

        public FeedViewer Viewer => _feedViewer.Create(this);
    }
}
