using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
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

        private readonly IFeedCommand _feedCommand;

        public Feed()
        {
            _feedViewerService = DependencyService.Get<IFeedViewerService>();
            _feedValidationService = DependencyService.Get<IFeedValidationService>();
            _feedCommand = DependencyService.Get<IFeedCommand>();

            Image = new UriImageSource { CachingEnabled = true, Uri = new Uri("https://img.olhardigital.com.br/wp-content/uploads/2021/04/PIX-2.jpg") };
        }

        public string Title { get; set; }

        public Uri Link { get; set; }

        public string Description { get; set; }

        public string Source { get; set; }

        public ImageSource Image { get; set; }

        public DateTimeOffset? PublishDate { get; set; }

        public DateTimeOffset? PublishDateLocal => PublishDate?.ToLocalTime();

        public TimeSpan PublishDuration => PublishDateLocal.HasValue ? (DateTimeOffset.Now - PublishDateLocal.Value) : TimeSpan.MaxValue;

        public FeedValidation Validation => _feedValidationService?.Create(this) ?? new FeedValidation();

        public FeedViewer Viewer => _feedViewerService?.Create(this) ?? new FeedViewer();

        public FeedCommand Command => _feedCommand?.Create(this) ?? new FeedCommand();
    }
}
