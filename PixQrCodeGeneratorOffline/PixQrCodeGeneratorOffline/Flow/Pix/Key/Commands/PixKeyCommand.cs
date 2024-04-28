using AsyncAwaitBestPractices.MVVM;
using pix_dynamic_payload_generator.net.Requests.RequestModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands
{
    public class PixKeyCommand : CommandBase, IPixKeyCommand
    {
        private readonly IPixPayloadService _pixPayloadService;

        public PixKeyCommand()
        {
            _pixPayloadService = DependencyService.Get<IPixPayloadService>();
        }

        public IAsyncCommand CopyKeyCommand { get; private set; }

        public IAsyncCommand ShareKeyCommand { get; private set; }

        public IAsyncCommand ShareOnWhatsCommand { get; private set; }

        public IAsyncCommand NavigateToCreateBillingPageCommand { get; private set; }

        public IAsyncCommand NavigateToCreateBillingDynamicPageCommand { get; private set; }

        public IAsyncCommand NavigateToPaymentPageCommand { get; private set; }

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
                NavigateToCreateBillingDynamicPageCommand = GetNavigateToCreateBillingDynamicCommand(pixKey),
                NavigateToPaymentPageCommand = GetNavigateToPaymentPageCommand(pixKey),
                EditKeyCommand = GetEdityKeyCommand(pixKey),
                NavigateToBillingCommand = GetNavigateToBillingCommand(pixKey)
            } : new PixKeyCommand();
        }

        private IAsyncCommand GetCopyKeyCommand(PixKey pixKey)
        {
            var message = $"Chave {pixKey?.Key}";
            message += pixKey.IsContact ? $" do contato {pixKey.Name}" : $" em {pixKey?.FinancialInstitution?.Institution?.Name}";
            message += " copiada.";

            var financialColors = pixKey?.FinancialInstitution?.Institution?.MaterialColor;

            return _customAsyncCommand.Create(async () => await _externalActionService.CopyText(pixKey?.Key, message , financialColors?.PrimaryDark, financialColors?.TextOnPrimary));
        }
            

        private IAsyncCommand GetShareKeyCommand(PixKey pixKey) =>
            _customAsyncCommand.Create(async () => await _externalActionService.ShareText(pixKey?.Key));

        private IAsyncCommand GetShareKeyOnWhatsCommand(PixKey pixKey) =>
            _customAsyncCommand.Create(async () => await _externalActionService.ShareOnWhats(pixKey?.Key));

        private IAsyncCommand GetNavigateToCreateBillingCommand(PixKey pixKey) 
        {
            return _customAsyncCommand.Create(async () => 
            { 
                SetIsLoading(); 
                
                await Shell.Current.Navigation.PushAsync(new CreateBillingPage(pixKey)).ConfigureAwait(false); 
                
                SetIsLoading(false); 
            });
        }

        private IAsyncCommand GetNavigateToCreateBillingDynamicCommand(PixKey pixKey)
        {
            return _customAsyncCommand.Create(async () =>
            {
                SetIsLoading();

                await Shell.Current.Navigation.PushAsync(new CreateBillingPage(pixKey, true)).ConfigureAwait(false);

                SetIsLoading(false);
            });
        }

        private IAsyncCommand GetNavigateToBillingCommand(PixKey pixKey)
        {
            return _customAsyncCommand.Create(async () =>
            {
                SetIsLoading();

                await Shell.Current.Navigation.PushAsync(new BillingSaveListPage(pixKey));

                SetIsLoading(false);
            });
        }
           

        private IAsyncCommand GetEdityKeyCommand(PixKey pixKey)
        {
            return _customAsyncCommand.Create(async () =>
            {
                SetIsLoading();

                await NavigateToEdit(pixKey, isContact: pixKey.IsContact);

                SetIsLoading(false);
            });
        }
            

        private IAsyncCommand GetNavigateToPaymentPageCommand(PixKey pixKey)
        {
            return _customAsyncCommand.Create(async () =>
            {
                await Shell.Current.Navigation.PushPopupAsync(new PaymentPage(_pixPayloadService.Create(pixKey))).ConfigureAwait(false);
            });
        }
            

        public async Task NavigateToEdit(PixKey pixKey, bool isContact = false)
        {
            if (!pixKey.IsValid())
                return;

            try
            {
                await Shell.Current.Navigation.PushPopupAsync(new AddPixKeyPage(pixKey, isContact));

                _eventService.SendEvent("Editou chave", EventType.UPDATE);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }
    }
}
