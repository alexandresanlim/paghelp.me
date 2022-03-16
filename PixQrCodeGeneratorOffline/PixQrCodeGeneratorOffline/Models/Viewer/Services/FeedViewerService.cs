using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;

namespace PixQrCodeGeneratorOffline.Models.Services.Viewer
{
    public class FeedViewerService : IFeedViewerService
    {
        public FeedViewer Create(Feed feed)
        {
            return new FeedViewer
            {
                PublishDateDisplay = feed.GetPublishDateDisplay(),
                PublishDateDisplayFull = feed.GetPublishDateDisplayFull()
            };
        }

        
    }
}
