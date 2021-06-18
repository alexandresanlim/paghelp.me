using PixQrCodeGeneratorOffline.Models.Validation;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using System;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class Feed
    {
        private readonly IFeedViewerService _feedViewerService;

        private readonly IFeedValidationService _feedValidationService;

        public Feed()
        {
            _feedViewerService = DependencyService.Get<IFeedViewerService>();
            _feedValidationService = DependencyService.Get<IFeedValidationService>();
        }

        public string Title { get; set; }

        public Uri Link { get; set; }

        public string Description { get; set; }

        public string Source { get; set; }

        public string Image { get; set; }

        public DateTimeOffset? PublishDate { get; set; }

        public DateTimeOffset? PublishDateLocal => PublishDate?.ToLocalTime();

        public TimeSpan PublishDuration => PublishDateLocal.HasValue ? (DateTimeOffset.Now - PublishDateLocal.Value) : TimeSpan.MaxValue;

        public FeedValidation Validation => _feedValidationService?.Create(this) ?? new FeedValidation();

        public FeedViewer Viewer => _feedViewerService?.Create(this) ?? new FeedViewer();
    }
}
