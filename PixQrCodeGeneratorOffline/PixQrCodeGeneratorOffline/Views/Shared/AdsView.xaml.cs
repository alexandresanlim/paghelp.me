
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views.Shared
{
    public partial class AdsView : Grid
    {
        public AdsView()
        {
            InitializeComponent();

            myAds.AdsId = App.Ids.GoogleAds;
        }
    }
}