
using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.Extention;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views.Content.StartPageContents
{
    public partial class StartPageMyKeysView : Grid
    {
        public StartPageMyKeysView()
        {
            InitializeComponent();
        }

        private void BoxView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is BoxView && e.PropertyName.Equals(nameof(BackgroundColor)))
            {
                bvKeys.RunOpacityAnimationAsync().SafeFireAndForget(ex => ex.SendToLog());
            }
        }
    }
}