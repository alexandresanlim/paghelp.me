using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands.Interfaces
{
    public interface IFeedCommand
    {
        FeedCommand Create(Feed feed);
    }
}
