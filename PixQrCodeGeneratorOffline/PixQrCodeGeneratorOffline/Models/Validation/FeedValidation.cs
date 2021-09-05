using PixQrCodeGeneratorOffline.Models.Validation.Services.Base;

namespace PixQrCodeGeneratorOffline.Models.Validation
{
    public class FeedValidation : ValidationBase
    {
        public bool IsToday { get; set; }

        public bool IsYesterday { get; set; }
    }
}
