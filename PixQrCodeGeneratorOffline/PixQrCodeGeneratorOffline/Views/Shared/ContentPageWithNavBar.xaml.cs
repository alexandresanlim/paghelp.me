using System;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views.Shared
{
    public partial class ContentPageWithNavBar : ContentPage
    {
        public ContentPageWithNavBar()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizerBackButtonPressed_Tapped(object sender, EventArgs e)
        {
            Shell.Current.SendBackButtonPressed();
        }
    }
}