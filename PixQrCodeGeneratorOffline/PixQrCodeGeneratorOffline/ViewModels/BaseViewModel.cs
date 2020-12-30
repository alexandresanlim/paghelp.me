using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

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

        #region Navigate

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public Command CloseModalCommand => new Command(async () =>
        {
            await CloseModal();
        });

        public Command NavigateToRootCommand => new Command(async () =>
        {
            await NavigateToRootAsync();
        });

        public async Task NavigateAsync(Page page)
        {
            await Shell.Current.Navigation.PushAsync(page);
        }

        public async Task NavigateModalAsync(Page page)
        {
            await Shell.Current.Navigation.PushModalAsync(page);
        }

        public async Task CloseModal()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        public async Task NavigateBack()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        public async Task NavigateToRootAsync()
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }

        #endregion

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
