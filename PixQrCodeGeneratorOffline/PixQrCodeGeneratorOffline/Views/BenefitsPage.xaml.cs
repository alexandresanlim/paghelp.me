using PixQrCodeGeneratorOffline.ViewModels;
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
    public partial class BenefitsPage : ContentPage
    {
        public BenefitsPage(bool hasKeys = false)
        {
            InitializeComponent();
            BindingContext = new BenefitsViewModel();
            xBtAddKey.IsVisible = !hasKeys;
        }
    }
}