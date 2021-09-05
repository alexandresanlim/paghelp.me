using PixQrCodeGeneratorOffline.Models.Validation.Services.Base;

namespace PixQrCodeGeneratorOffline.Models.Validation
{
    public class PixKeyValidation : ValidationBase
    {
        public bool HasKey { get; set; }

        public bool HasName { get; set; }

        public bool IsEdit { get; set; }
    }
}
