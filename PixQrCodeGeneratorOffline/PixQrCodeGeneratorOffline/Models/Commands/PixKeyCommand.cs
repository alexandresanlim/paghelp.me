using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using PixQrCodeGeneratorOffline.Views;
using PixQrCodeGeneratorOffline.Views.Shared;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands
{
    public class PixKeyCommand : CommandBase, IPixKeyCommand
    {
        private readonly IExternalActionService _externalActionService;

        private readonly IPixPayloadService _pixPayloadService;

        private readonly IPixKeyService _pixKeyService;

        public PixKeyCommand()
        {
            _externalActionService = DependencyService.Get<IExternalActionService>();
            _pixPayloadService = DependencyService.Get<IPixPayloadService>();
            _pixKeyService = DependencyService.Get<IPixKeyService>();
        }

        public ICommand CopyKeyCommand { get; private set; }

        public ICommand ShareKeyCommand { get; private set; }

        public ICommand ShareOnWhatsCommand { get; private set; }

        public ICommand NavigateToCreateBillingPageCommand { get; private set; }

        public ICommand NavigateToPaymentPageCommand { get; private set; }

        public ICommand NavigateToDownloadQrCodeCommand { get; private set; }

        public ICommand EditKeyCommand { get; private set; }

        public ICommand NavigateToBillingCommand { get; private set; }

        public PixKeyCommand Create(PixKey pixKey)
        {
            return pixKey.Validation.IsValid ? new PixKeyCommand
            {
                CopyKeyCommand = GetCopyKeyCommand(pixKey),
                ShareKeyCommand = GetShareKeyCommand(pixKey),
                ShareOnWhatsCommand = GetShareKeyOnWhatsCommand(pixKey),
                NavigateToCreateBillingPageCommand = GetNavigateToCreateBillingCommand(pixKey),
                NavigateToPaymentPageCommand = GetNavigateToPaymentPageCommand(pixKey),
                EditKeyCommand = GetEdityKeyCommand(pixKey),
                NavigateToBillingCommand = GetNavigateToBillingCommand(pixKey),
                NavigateToDownloadQrCodeCommand = GetNavigateToDownloadQrCodeCommand(pixKey),
            } : new PixKeyCommand();
        }

        private Command GetCopyKeyCommand(PixKey pixKey)
        {
            return new Command(async () => await _externalActionService.CopyText(pixKey?.Key, "Chave copiada com sucesso!"));
        }

        private Command GetShareKeyCommand(PixKey pixKey)
        {
            return new Command(async () =>
            {
                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    await _externalActionService.ShareText(pixKey?.Key);
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Compartilhou chave", EventType.SHARE);

                    DialogService.HideLoading();
                }
            });
        }

        private Command GetShareKeyOnWhatsCommand(PixKey pixKey)
        {
            return new Command(async () =>
            {
                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    await _externalActionService.ShareOnWhats(pixKey?.Key);
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Compartilhou chave no WhatsApp", EventType.SHARE);

                    DialogService.HideLoading();
                }
            });
        }

        private Command GetNavigateToCreateBillingCommand(PixKey pixKey)
        {
            return new Command(async () =>
            {
                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    await Shell.Current.Navigation.PushModalAsync(new CreateBillingTabbedPage(pixKey));
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Navegou para criação de cobrança", EventType.NAVIGATION);

                    DialogService.HideLoading();
                }
            });
        }

        public bool IsLoad { get; set; } = false;

        private Command GetNavigateToPaymentPageCommand(PixKey pixKey)
        {
            return new Command(async () =>
            {
                try
                {
                    if (IsLoad)
                        return;

                    IsLoad = true;

                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    var pixPaylod = _pixPayloadService.Create(pixKey);

                    await Shell.Current.Navigation.PushAsync(new PaymentPage(pixPaylod));
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Navegou para pagina de pagamento a partir da dashboard", EventType.NAVIGATION);

                    DialogService.HideLoading();

                    IsLoad = false;
                }
            });
        }

        private Command GetNavigateToDownloadQrCodeCommand(PixKey pixKey)
        {
            return new Command(async () =>
            {
                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    await Shell.Current.Navigation.PushAsync(new WebViewPage(new System.Uri("https://chart.googleapis.com/chart?chs=400x400&cht=qr&chl=" + pixKey?.Payload?.QrCode)));
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Navegou para baixar o Qr Code", EventType.NAVIGATION);

                    DialogService.HideLoading();
                }
            });
        }

        private Command GetNavigateToBillingCommand(PixKey pixKey)
        {
            return new Command(async () =>
            {
                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    await Shell.Current.Navigation.PushAsync(new BillingSaveListPage(pixKey));
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Navegou para pagina de cobranças salvas a partir da dashboard", EventType.NAVIGATION);

                    DialogService.HideLoading();
                }
            });
        }

        private Command GetEdityKeyCommand(PixKey pixKey)
        {
            return new Command(async () => await _pixKeyService.NavigateToEdit(pixKey, isContact: pixKey.IsContact));
        }
    }
}
