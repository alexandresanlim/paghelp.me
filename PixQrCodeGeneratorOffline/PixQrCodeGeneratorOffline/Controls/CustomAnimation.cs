using Lottie.Forms;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomAnimation : AnimationView
    {
        public CustomAnimation()
        {
            Loop = true;
            IsPlaying = true;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
