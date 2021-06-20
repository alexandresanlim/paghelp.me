using PixQrCodeGeneratorOffline.ViewModels;
using PixQrCodeGeneratorOffline.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionKeyPage : ContentPageWithNavBar
    {
        OptionKeyViewModel _optionKeyViewModel;

        public OptionKeyPage()
        {
            InitializeComponent();

            BindingContext = _optionKeyViewModel = new OptionKeyViewModel();
        }
    }
}