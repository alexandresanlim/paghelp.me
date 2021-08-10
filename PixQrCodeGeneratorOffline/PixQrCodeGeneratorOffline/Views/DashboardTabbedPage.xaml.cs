using PixQrCodeGeneratorOffline.Models.Services;
using PixQrCodeGeneratorOffline.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardTabbedPage : TabbedPage
    {
        public DashboardTabbedPage()
        {
            InitializeComponent();
        }

        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            //var tabbed = (TabbedPage)sender;

            //if (tabbed.CurrentPage is DashboardPage)
            //{
            //    var vm = (DashboardViewModel)tabbed.CurrentPage.BindingContext;
            //    vm.SetStatusFromCurrentPixColor();
            //}

            //else
            //{
            //    App.LoadTheme();
            //}
        }
    }
}