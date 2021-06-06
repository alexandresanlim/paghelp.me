using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Viewer.Interfaces
{
    public interface IFeedViewer
    {
        FeedViewer Create(Feed feed);
    }
}
