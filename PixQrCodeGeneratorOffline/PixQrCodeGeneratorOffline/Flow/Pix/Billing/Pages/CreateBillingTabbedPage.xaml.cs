using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.ViewModels;
using System;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class CreateBillingTabbedPage : TabbedPage
    {
        private static PixKey PixKey { get; set; }

        public CreateBillingTabbedPage(PixKey pixKey)
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
            if (sender is TabbedPage tabbed)
            {
                if (!(PixKey?.Id > 0))
                    return;

                if (tabbed.CurrentPage is CreateBillingPage && tabbed.CurrentPage.BindingContext is CreateBillingViewModel vm)
                {
                    vm.LoadDataCommand.Execute(PixKey);
                    return;
                }

                if (tabbed.CurrentPage is CreateBillingSavePage && tabbed.CurrentPage.BindingContext is CreateBillingSaveViewModel saveVM)
                {
                    saveVM.LoadDataCommand.Execute(PixKey);
                    return;
                }
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