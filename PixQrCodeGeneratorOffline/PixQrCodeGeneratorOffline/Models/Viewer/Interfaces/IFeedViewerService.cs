using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Viewer.Interfaces
{
    public interface IFeedViewerService
    {
        string GetPublishDateDisplay(Feed feed);

        string GetPublishDateDisplayFull(Feed feed);
    }
}
