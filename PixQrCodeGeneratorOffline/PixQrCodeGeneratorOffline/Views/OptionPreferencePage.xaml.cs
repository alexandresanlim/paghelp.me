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
    public partial class OptionPreferencePage : ContentPage
    {
        OptionPreferenceViewModel _optionPreferenceViewModel;

        public OptionPreferencePage()
        {
            InitializeComponent();

            BindingContext = _optionPreferenceViewModel = new OptionPreferenceViewModel();
        }

        private bool IsOppearing { get; set; }

        protected override void OnAppearing()
        {
            try
            {
                IsOppearing = true;

                _optionPreferenceViewModel.LoadData();
                base.OnAppearing();
            }
            catch (Exception)
            {
            }
            finally
            {
                IsOppearing = false;
            }
        }

        private async void Switch_News(object sender, ToggledEventArgs e)
        {
            if (IsOppearing)
                return;

            await _optionPreferenceViewModel.OptionShowNews();
        }

        private async void Switch_FingerPrint(object sender, ToggledEventArgs e)
        {
            if (IsOppearing)
                return;

            await _optionPreferenceViewModel.OptionFingerPrint();
        }

        private async void Switch_PdvMode(object sender, ToggledEventArgs e)
        {
            if (IsOppearing)
                return;

            await _optionPreferenceViewModel.OptionPDV();
        }

        private async void Switch_Theme(object sender, ToggledEventArgs e)
        {
            if (IsOppearing)
                return;

            await _optionPreferenceViewModel.OptionTheme();
        }

        private void Switch_Cypto(object sender, ToggledEventArgs e)
        {

        }
    }
}