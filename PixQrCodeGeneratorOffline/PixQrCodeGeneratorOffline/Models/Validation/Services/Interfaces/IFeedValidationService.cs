using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces
{
    public interface IFeedValidationService
    {
        FeedValidation Create(Feed feed);
    }
}
