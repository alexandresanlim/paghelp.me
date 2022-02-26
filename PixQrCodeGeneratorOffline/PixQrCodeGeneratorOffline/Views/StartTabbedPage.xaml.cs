using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.Extention;
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

            xTitle.Text = DateTimeExtention.GetDashboardTitleFromPeriod();
            xSubTitle.Text = DateTimeExtention.GetDashboardSubtitleFromDayOfWeed();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AddPages();

            if (App.StatusBarService != null && App.ThemeColors?.PrimaryDark != null)
                App.StatusBarService.SetStatusBarColor(App.ThemeColors.PrimaryDark);
        }

        private void AddPages()
        {
            if (Children.Count.Equals(2) && Services.Preference.CryptoAble || (Children.Count.Equals(3) && !Services.Preference.CryptoAble))
                Children.Clear();

            if (Children.Count.Equals(0))
            {
                Children.Add(new StartPage { Title = "Pix" });

                if (Services.Preference.CryptoAble)
                    Children.Add(new StartCryptoPage { Title = "Cripto (Beta)" });

                Children.Add(new StartMorePage { Title = "Mais" });
            }
        }

        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            var tabbed = (TabbedPage)sender;

            if (tabbed.CurrentPage is StartCryptoPage || tabbed.CurrentPage is StartPage)
            {
                var vm = (DashboardViewModelBase)tabbed.CurrentPage.BindingContext;
                vm.LoadHideValue();
            }

            //else
            //{
            //    App.LoadTheme();
            //}
        }
    }
}