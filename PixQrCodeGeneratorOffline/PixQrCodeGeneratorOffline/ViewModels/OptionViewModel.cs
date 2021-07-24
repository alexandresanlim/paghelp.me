using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class OptionViewModel : ViewModelBase
    {
        public ICommand OpenPreferencesOptionsCommand => new Command(async () => await NavigateAsync(new OptionPreferencePage()));

        public ICommand OpenKeysOptionsCommand => new Command(async () => await NavigateAsync(new OptionKeyPage()));

        public ICommand OpenOptionBillingPageCommand => new Command(async () => await NavigateAsync(new OptionBillingPage()));

        public OptionViewModel()
        {
            SaveCommand = new Command(() =>
            {

            });
        }
    }
}
