using PixQrCodeGeneratorOffline.ViewModels;
using System;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class CreateBillingTabbedPage : TabbedPage
    {
        private static Models.PaymentMethods.Pix.PixKey PixKey { get; set; }

        public CreateBillingTabbedPage(Models.PaymentMethods.Pix.PixKey pixKey)
        {
            PixKey = pixKey;

            InitializeComponent();

            var primaryDark = PixKey?.FinancialInstitution?.Institution?.MaterialColor?.PrimaryDark ?? App.ThemeColors.PrimaryDark;

            tb.BarBackgroundColor = primaryDark;
            tb.SelectedTabColor = PixKey?.FinancialInstitution?.Institution?.MaterialColor?.TextOnPrimary ?? App.ThemeColors.TextOnPrimary;

            App.StatusBarService.SetStatusBarColor(primaryDark);
        }

        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            var tabbed = (TabbedPage)sender;

            if (tabbed.CurrentPage is CreateBillingPage)
            {
                var vm = (CreateBillingViewModel)tabbed.CurrentPage.BindingContext;

                if (vm?.CurrentPixKey?.Id > 0)
                    return;

                vm.LoadDataCommand.Execute(PixKey);
            }

            if (tabbed.CurrentPage is CreateBillingSavePage)
            {
                var vm = (CreateBillingSaveViewModel)tabbed.CurrentPage.BindingContext;

                if (vm?.CurrentPixKey?.Id > 0)
                    return;

                vm.LoadDataCommand.Execute(PixKey);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            ReloadStatusBar();
            return base.OnBackButtonPressed();
        }

        private void ReloadStatusBar()
        {
            App.StatusBarService.SetStatusBarColor(App.ThemeColors.PrimaryDark);
        }
    }
}