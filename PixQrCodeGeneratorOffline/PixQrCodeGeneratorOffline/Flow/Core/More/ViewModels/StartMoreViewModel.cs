using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class StartMoreViewModel : ViewModelBase
    {
        public string CurrentVersion => App.Info.VersionString;

        #region Commands

        public IAsyncCommand NavigateToPreferencesCommand => new AsyncCommand(async () => await NavigateAsync(new OptionPreferencePage()));

        public IAsyncCommand NavigateBenefitsCommand => new AsyncCommand(async () => await NavigateAsync(new BenefitsPage(true)));

        public ICommand LoadDataCommand => new Command(LoadData);

        public IAsyncCommand SendAMessageCommand => new AsyncCommand(NavigateToLikingPage);

        public ICommand OpenStoreCommand => new Command(OpenStoreAsync);

        #endregion

        private void LoadData()
        {
            if (App.DeviceInfo.IsAndroid)
            {
                CurrentStore = "Avaliar na Google Play";
                CurrentStoreIcon = FontAwesomeBrands.GooglePlay;
            }

            else
            {
                CurrentStore = "Avaliar na App Store";
                CurrentStoreIcon = FontAwesomeBrands.AppStore;
            }
        }

        private void OpenStoreAsync()
        {
            try
            {
                App.OpenAppInStore();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Tocou em avaliar", Services.EventType.TAP, nameof(StartMoreViewModel));
            }
        }

        private string _currentStore;
        public string CurrentStore
        {
            set => SetProperty(ref _currentStore, value);
            get => _currentStore;
        }

        private string _currentStoreIcon;
        public string CurrentStoreIcon
        {
            set => SetProperty(ref _currentStoreIcon, value);
            get => _currentStoreIcon;
        }
    }
}
