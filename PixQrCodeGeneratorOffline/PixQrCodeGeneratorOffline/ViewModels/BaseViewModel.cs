using Acr.UserDialogs;
using Microsoft.AppCenter.Analytics;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            ShowAds = true;

            Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        }

        private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            ReloadAppColorIfShowInListStyle();
        }

        public void ReloadAppColorIfShowInListStyle()
        {
            if (PreferenceService.ShowInList)
            {
                var colorSystem = (AppInfo.RequestedTheme == AppTheme.Light || AppInfo.RequestedTheme == AppTheme.Unspecified) ? Style.MaterialColor.GetLightColors() : Style.MaterialColor.GetDarkColors();
                App.LoadTheme(colorSystem, PreferenceService.ShowInList);
            }
        }

        public IUserDialogs DialogService => UserDialogs.Instance;

        public async Task ShareText(string text)
        {
            await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareTextRequest
            {
                Text = text,
                Title = "Escolha uma opção"
            });
        }

        public async Task CopyText(string text, string textSuccess = "Copiado com sucesso!")
        {
            await Xamarin.Essentials.Clipboard.SetTextAsync(text);
            DialogService.Toast(textSuccess);
        }

        #region Loading

        bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            private set { SetProperty(ref isLoading, value); }
        }

        public void SetIsLoading(bool isLoading = true, string title = "")
        {
            IsLoading = isLoading;

            if (IsLoading)
                DialogService.ShowLoading(title);

            else
                DialogService.HideLoading();
        }

        #endregion

        public void SetEvent(string text)
        {
            Analytics.TrackEvent(text);
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        #region Navigate

        public Command NavigateBackCommand => new Command(() =>
        {
            NavigateBack();
        });

        public Command NavigateToRootCommand => new Command(async () =>
        {
            await NavigateToRootAsync();
        });

        public async Task NavigateAsync(Page page)
        {
            await Shell.Current.Navigation.PushAsync(page, true);
        }

        public async Task NavigateModalAsync(Page page)
        {
            await Shell.Current.Navigation.PushModalAsync(page, true);
        }

        public void NavigateBack()
        {
            Shell.Current.SendBackButtonPressed();
        }

        //public async Task NavigateBackModalAsync()
        //{
        //    await Shell.Current.Navigation.PopModalAsync();
        //}

        //public async Task NavigateBackAsync()
        //{
        //    await Shell.Current.Navigation.PopAsync();
        //}

        public async Task NavigateToRootAsync()
        {
            await Shell.Current.Navigation.PopToRootAsync(true);
        }

        #endregion

        public ICommand CloseAdsCommand => new Command(() =>
        {
            ShowAds = false;
        });

        private bool _showAds;
        public bool ShowAds
        {
            get => _showAds;
            set => SetProperty(ref _showAds, value);
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
