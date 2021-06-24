using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public void LoadData()
        {
            CurrentPixKey = _pixKeyService.GetById(CurrentPixKey.Id);
            _statusBarService.SetStatusBarColor(CurrentPixKey.FinancialInstitution.Institution.MaterialColor.Primary);
        }

        public ICommand EditKeyCommand => new Command(async () =>
        {
            var pixKey = CurrentPixKey;
            await _pixKeyService.NavigateToEdit(DashboardVM, pixKey);
        });

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }
    }
}
