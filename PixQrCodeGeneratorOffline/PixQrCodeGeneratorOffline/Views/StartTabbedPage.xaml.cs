using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.ViewModels.Base;
using System;
using System.Reflection;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class StartTabbedPage : TabbedPage
    {
        public StartTabbedPage()
        {
            InitializeComponent();

            xTitle.Text = DateTimeExtention.GetDashboardTitleFromPeriod();
            xSubTitle.Text = DateTimeExtention.GetDashboardSubtitleFromDayOfWeed();
        }

        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            var tabbed = (TabbedPage)sender;

            if (tabbed.CurrentPage is StartCryptoPage || tabbed.CurrentPage is StartPage)
            {
                var vm = (DashboardViewModelBase)tabbed.CurrentPage.BindingContext;
                vm.LoadHideValue().SafeFireAndForget();
            }

            //else
            //{
            //    App.LoadTheme();
            //}
        }
    }
}