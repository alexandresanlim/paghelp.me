using PixQrCodeGeneratorOffline.Models.Validation.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Validation
{
    public class FeedValidation : ValidationBase
    {
        public bool IsToday { get; set; }
    }
}
