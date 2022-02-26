using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto
{
    public class CryptoKeyCommand : CommandBase, ICryptoKeyCommand
    {
        private readonly ICryptoPayloadService _cryptoPayloadService;

        private readonly ICryptoKeyService _cryptoKeyService;

        public CryptoKeyCommand()
        {
            _cryptoPayloadService = DependencyService.Get<ICryptoPayloadService>();
            _cryptoKeyService = DependencyService.Get<ICryptoKeyService>();
        }

        public ICommand CopyKeyCommand { get; private set; }

        public ICommand ShareKeyCommand { get; private set; }

        public ICommand ShareOnWhatsCommand { get; private set; }

        //public ICommand NavigateToCreateBillingPageCommand { get; private set; }

        public ICommand NavigateToPaymentPageCommand { get; private set; }

        //public ICommand NavigateToDownloadQrCodeCommand { get; private set; }

        public ICommand EditKeyCommand { get; private set; }

        // public ICommand NavigateToBillingCommand { get; private set; }

        public CryptoKeyCommand Create(CryptoKey cryptoKey)
        {
            return cryptoKey.IsValid() ? new CryptoKeyCommand
            {
                CopyKeyCommand = GetCopyKeyCommand(cryptoKey),
                ShareKeyCommand = GetShareKeyCommand(cryptoKey),
                ShareOnWhatsCommand = GetShareKeyOnWhatsCommand(cryptoKey),
                //NavigateToCreateBillingPageCommand = GetNavigateToCreateBillingCommand(pixKey),
                NavigateToPaymentPageCommand = GetNavigateToPaymentPageCommand(cryptoKey),
                EditKeyCommand = GetEdityKeyCommand(cryptoKey),
                //NavigateToBillingCommand = GetNavigateToBillingCommand(pixKey),
                //NavigateToDownloadQrCodeCommand = GetNavigateToDownloadQrCodeCommand(pixKey),
            } : new CryptoKeyCommand();
        }

        private Command GetCopyKeyCommand(CryptoKey pixKey)
        {
            return new Command(async () => await _externalActionService.CopyText(pixKey?.Key, $"Chave {pixKey?.Key} de {pixKey?.FinancialInstitution?.Institution?.Name} copiada", pixKey?.FinancialInstitution?.Institution?.MaterialColor?.PrimaryDark, pixKey?.FinancialInstitution?.Institution?.MaterialColor?.TextOnPrimary));
        }

        private Command GetShareKeyCommand(CryptoKey pixKey)
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

        private Command GetShareKeyOnWhatsCommand(CryptoKey pixKey)
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

        private Command GetEdityKeyCommand(CryptoKey pixKey)
        {
            return new Command(async () => await _cryptoKeyService.NavigateToEdit(pixKey, isContact: pixKey.IsContact));
        }

        public bool IsLoad { get; set; } = false;

        private Command GetNavigateToPaymentPageCommand(CryptoKey pixKey)
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

                    var pixPaylod = _cryptoPayloadService.Create(pixKey);

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
    }
}
