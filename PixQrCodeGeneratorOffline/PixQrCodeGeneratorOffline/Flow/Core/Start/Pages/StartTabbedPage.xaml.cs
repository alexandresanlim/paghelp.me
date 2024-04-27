using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto;
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

            if (tabbed.CurrentPage is StartCryptoPage)
            {
                //SetCryptoStatusBarColor();
            }
            else
            {
                SetPixStatusBarColor();
            }

            if (tabbed.CurrentPage is StartCryptoPage || tabbed.CurrentPage is StartPage)
            {
                var vm = (DashboardViewModelBase)tabbed.CurrentPage.BindingContext;
                vm.LoadHideValue();
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PushAsync(new OptionPreferencePage(), true);
        }

        private void SetCryptoStatusBarColor()
        {
            var btcColor = new Bitcoin().MaterialColor.Primary;

            SetStatusBarColor(btcColor);
            xHeader.BackgroundColor = BarBackgroundColor = btcColor;
        }

        private void SetPixStatusBarColor()
        {
            if (App.ThemeColors?.PrimaryDark != null)
            { 
                SetStatusBarColor(App.ThemeColors.PrimaryDark);
                xHeader.BackgroundColor = BarBackgroundColor = App.ThemeColors.PrimaryDark;
            }
        }

        private void SetStatusBarColor(Color color)
        {
            if (App.StatusBarService != null)
                App.StatusBarService.SetStatusBarColor(color);
        }
    }
}