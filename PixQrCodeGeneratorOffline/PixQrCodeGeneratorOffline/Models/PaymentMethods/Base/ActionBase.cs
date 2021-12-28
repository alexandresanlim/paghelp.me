using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using Xamarin.Forms;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Base
{
    public class ActionBase
    {
        public string Icon { get; set; }

        public string Title { get; set; }

        public MaterialColor Colors { get; set; }

        public FontAwesomeType IconType { get; set; } = FontAwesomeType.solid;

        public KeyActionType Type { get; set; }
    }
}
