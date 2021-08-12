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
    public partial class CreateBillingTabbedPage : TabbedPage
    {
        private static Models.PixKey PixKey { get; set; }

        public CreateBillingTabbedPage(Models.PixKey pixKey)
        {
            PixKey = pixKey;

            App.StatusBarService.SetStatusBarColor(pixKey.FinancialInstitution.Institution.MaterialColor.Primary);

            InitializeComponent();

            tb.BarBackgroundColor = PixKey?.FinancialInstitution?.Institution?.MaterialColor?.Primary ?? App.ThemeColors.Primary;
            tb.SelectedTabColor = PixKey?.FinancialInstitution?.Institution?.MaterialColor?.TextOnPrimary ?? App.ThemeColors.TextOnPrimary;
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
            App.StatusBarService.SetStatusBarColor(App.ThemeColors.Primary);
        }
    }
}