using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.ViewModels.Base;
using System;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class StartTabbedPage : TabbedPage
    {
        public StartTabbedPage()
        {
            InitializeComponent();
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