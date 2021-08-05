using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class OptionBillingViewModel : ViewModelBase
    {
        public ICommand RemoveAllCommand => new Command(async () => await _pixPayloadService.RemoveAll());

        public ICommand ShowAllCommand => new Command(async () => await NavigateAsync(new BillingSaveListPage()));
    }
}
