using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using System;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class Feed
    {
        private readonly IFeedViewerService _feedViewerService;

        public Feed()
        {
            _feedViewerService = DependencyService.Get<IFeedViewerService>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public Uri Link { get; set; }

        public DateTimeOffset? PublishDate { get; set; }

        public string Description { get; set; }

        public string Source { get; set; }

        public string FirstImage { get; set; }

        public bool WasRead { get; set; }

        public int Index { get; set; }

        public DateTimeOffset? PublishDateLocal => PublishDate?.ToLocalTime();

        public bool IsToday => DateTimeOffset.Now.Date.Equals(PublishDateLocal.Value.Date);

        public TimeSpan PublishDuration => PublishDateLocal.HasValue ? (DateTimeOffset.Now - PublishDateLocal.Value) : TimeSpan.MaxValue;

        public FeedViewer Viewer => _feedViewerService.Create(this);
    }
}
