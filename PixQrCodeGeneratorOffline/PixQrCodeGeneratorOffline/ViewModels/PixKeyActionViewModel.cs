using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class PixKeyActionViewModel : BaseViewModel
    {
        public DashboardViewModel DashboardVM { get; set; }

        public PixKeyActionViewModel(DashboardViewModel dashboardVM, PixKey pixKey)
        {
            DashboardVM = dashboardVM;
            CurrentPixKey = pixKey;
        }

        public ICommand EditKeyCommand => new Command(async () => await _pixKeyService.NavigateToEdit(DashboardVM, CurrentPixKey));

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }
    }
}
