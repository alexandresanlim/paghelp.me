using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.ViewModels;
using System;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
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

                _optionPreferenceViewModel.LoadData().SafeFireAndForget();

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

        private void Switch_News(object sender, ToggledEventArgs e)
        {
            if (IsOppearing)
                return;

            //_optionPreferenceViewModel.OptionShowNews().SafeFireAndForget();
        }

        private void Switch_FingerPrint(object sender, ToggledEventArgs e)
        {
            if (IsOppearing)
                return;

            _optionPreferenceViewModel.OptionFingerPrint().SafeFireAndForget();
        }

        private void Switch_PdvMode(object sender, ToggledEventArgs e)
        {
            if (IsOppearing)
                return;

            _optionPreferenceViewModel.OptionPDV().SafeFireAndForget();
        }

        private void Switch_Theme(object sender, ToggledEventArgs e)
        {
            if (IsOppearing)
                return;

            _optionPreferenceViewModel.OptionTheme().SafeFireAndForget();
        }

        private void Switch_Cypto(object sender, ToggledEventArgs e)
        {
            if (IsOppearing)
                return;

            _optionPreferenceViewModel.ChangeCrypto().SafeFireAndForget();
        }

        
    }
}