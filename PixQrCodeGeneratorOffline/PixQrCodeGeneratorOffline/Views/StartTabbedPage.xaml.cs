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