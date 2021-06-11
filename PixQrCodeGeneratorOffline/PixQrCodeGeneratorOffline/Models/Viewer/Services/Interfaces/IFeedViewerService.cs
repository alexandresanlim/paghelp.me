using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces
{
    public interface IFeedViewerService
    {
        FeedViewer Create(Feed feed);
    }
}
