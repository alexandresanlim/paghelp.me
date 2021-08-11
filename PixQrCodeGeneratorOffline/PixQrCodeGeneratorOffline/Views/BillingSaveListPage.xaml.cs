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
    public partial class BillingSaveListPage : ContentPageWithNavBar
    {
        public BillingSaveListPage(Models.PixKey pixKey = null)
        {
            InitializeComponent();
            BindingContext = new BillingSaveListViewModel(pixKey);
        }
    }
}