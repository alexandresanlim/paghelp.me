using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BackButtonView : ContentView
    {
        public BackButtonView()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Shell.Current.SendBackButtonPressed();
        }
    }
}