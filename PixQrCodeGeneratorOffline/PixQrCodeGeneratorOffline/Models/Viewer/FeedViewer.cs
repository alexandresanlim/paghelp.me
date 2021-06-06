using PixQrCodeGeneratorOffline.Models.Viewer.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Viewer
{
    public class FeedViewer : IFeedViewer
    {
        private readonly IFeedViewerService _feedViewService;

        public FeedViewer()
        {
            _feedViewService = DependencyService.Get<IFeedViewerService>();
        }

        public string PublishDateDisplayFull { get; private set; }

        public string PublishDateDisplay { get; private set; }

        public FeedViewer Create(Feed feed)
        {
            return new FeedViewer
            {
                PublishDateDisplay = _feedViewService.GetPublishDateDisplay(feed),
                PublishDateDisplayFull = _feedViewService.GetPublishDateDisplayFull(feed)
            };
        }
    }
}
