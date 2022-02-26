using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using PixQrCodeGeneratorOffline.Views.Shared;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands
{
    public class PixKeyCommand : CommandBase, IPixKeyCommand
    {
        private readonly IPixPayloadService _pixPayloadService;

        //private readonly IPixKeyService _pixKeyService;

        public PixKeyCommand()
        {
            _pixPayloadService = DependencyService.Get<IPixPayloadService>();
            //_pixKeyService = DependencyService.Get<IPixKeyService>();
        }

        public IAsyncCommand CopyKeyCommand { get; private set; }

        public IAsyncCommand ShareKeyCommand { get; private set; }

        public IAsyncCommand ShareOnWhatsCommand { get; private set; }

        public IAsyncCommand NavigateToCreateBillingPageCommand { get; private set; }

        public IAsyncCommand NavigateToPaymentPageCommand { get; private set; }

        public IAsyncCommand NavigateToDownloadQrCodeCommand { get; private set; }

        public IAsyncCommand EditKeyCommand { get; private set; }

        public IAsyncCommand NavigateToBillingCommand { get; private set; }

        public PixKeyCommand Create(PixKey pixKey)
        {
            return pixKey.IsValid() ? new PixKeyCommand
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

        private IAsyncCommand GetCopyKeyCommand(PixKey pixKey) =>
            _customAsyncCommand.Create(async () => await _externalActionService.CopyText(pixKey?.Key, $"Chave {pixKey?.Key} em {pixKey?.FinancialInstitution?.Institution?.Name} copiada.", pixKey?.FinancialInstitution?.Institution?.MaterialColor?.PrimaryDark, pixKey?.FinancialInstitution?.Institution?.MaterialColor?.TextOnPrimary));

        private IAsyncCommand GetShareKeyCommand(PixKey pixKey) =>
            _customAsyncCommand.Create(async () => await _externalActionService.ShareText(pixKey?.Key));

        private IAsyncCommand GetShareKeyOnWhatsCommand(PixKey pixKey) =>
            _customAsyncCommand.Create(async () => await _externalActionService.ShareOnWhats(pixKey?.Key));

        private IAsyncCommand GetNavigateToCreateBillingCommand(PixKey pixKey) =>
            _customAsyncCommand.Create(async () => await Shell.Current.Navigation.PushAsync(new CreateBillingTabbedPage(pixKey)));

        private IAsyncCommand GetNavigateToBillingCommand(PixKey pixKey) =>
            _customAsyncCommand.Create(async () => await Shell.Current.Navigation.PushAsync(new BillingSaveListPage(pixKey)));

        private IAsyncCommand GetEdityKeyCommand(PixKey pixKey) =>
            _customAsyncCommand.Create(async () => await NavigateToEdit(pixKey, isContact: pixKey.IsContact));

        private IAsyncCommand GetNavigateToPaymentPageCommand(PixKey pixKey) =>
            _customAsyncCommand.Create(async () => await Shell.Current.Navigation.PushAsync(new PaymentPage(_pixPayloadService.Create(pixKey))));

        private IAsyncCommand GetNavigateToDownloadQrCodeCommand(PixKey pixKey) =>
            _customAsyncCommand.Create(async () => await Shell.Current.Navigation.PushAsync(new WebViewPage(new System.Uri("https://chart.googleapis.com/chart?chs=400x400&cht=qr&chl=" + pixKey?.Payload?.QrCode))));

        public async Task NavigateToEdit(PixKey pixKey, bool isContact = false)
        {
            if (!pixKey.IsValid())
                return;

            try
            {
                await Shell.Current.Navigation.PushAsync(new AddPixKeyPage(pixKey, isContact));

                _eventService.SendEvent("Editou chave", EventType.UPDATE);
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        }
    }
}
