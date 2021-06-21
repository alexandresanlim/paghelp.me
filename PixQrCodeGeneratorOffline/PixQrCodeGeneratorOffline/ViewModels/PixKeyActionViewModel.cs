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

        public ICommand EditKeyCommand => new Command(async () => await _pixKeyService.NavigateToEdit(DashboardVM, CurrentPixKey));

        public ICommand CopyKeyCommand => new Command(async () => await _externalActionService.CopyText(CurrentPixKey?.Key, "Chave copiada com sucesso!"));

        public ICommand ShareKeyCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                await _externalActionService.ShareText(CurrentPixKey?.Key);
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Compartilhou chave", EventType.SHARE);

                SetIsLoading(false);
            }
        });

        public ICommand NavigateToCreateBillingPageCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                await NavigateModalAsync(new CreateBillingPage(CurrentPixKey));
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Navegou para criação de cobrança", EventType.NAVIGATION);

                SetIsLoading(false);
            }
        });

        public ICommand NavigateToPaymentPageCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                var pixPaylod = _pixPayloadService.Create(CurrentPixKey);

                await NavigateModalAsync(new PaymentPage(pixPaylod));
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Navegou para pagina de pagamento a partir da dashboard", EventType.NAVIGATION);

                SetIsLoading(false);
            }
        });

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }
    }
}
