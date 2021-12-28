using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class PaymentViewModel : ViewModelBase
    {
        #region Commands

        public IAsyncCommand SaveCommand => new AsyncCommand(Save);

        public ICommand LoadDataCommand => new Command<PayloadBase>((payloadParameter) => LoadData(payloadParameter));

        public ICommand ChangeIsActionVisibleCommand => new Command(ChangeIsActionVisible);

        #endregion

        private void LoadData(PayloadBase payloadParameter)
        {
            CurrentPaylodBase = payloadParameter;
            CurrentInfo = new PaymentViewModelInfo();

            if (payloadParameter is PixPayload pixPayload)
            {
                CurrentPixPaylod = pixPayload;
                CurrentInfo.Color = pixPayload.PixKey?.FinancialInstitution?.Institution?.MaterialColor;
                CurrentInfo.Value = pixPayload.PixCob?.Viewer?.ValuePresentation;
                CurrentInfo.Name = pixPayload.PixKey?.Viewer?.NamePresentation;
                CurrentInfo.Institution = $"Instituição: {pixPayload?.PixKey?.Viewer?.InstitutionPresentation}";
                CurrentInfo.Key = $"Chave: {pixPayload?.PixKey?.Viewer?.KeyPresentation}";
            }

            else if (payloadParameter is CryptoPayload cryptoPayload)
            {
                CurrentCryptoPaylod = cryptoPayload;
                CurrentInfo.Color = cryptoPayload?.CryptoKey?.FinancialInstitution?.Institution?.MaterialColor;
                CurrentInfo.Institution = $"Criptomoeda: {cryptoPayload?.CryptoKey?.Viewer?.InstitutionPresentation}";
                CurrentInfo.Key = $"Chave: {cryptoPayload?.CryptoKey?.Viewer?.KeyPresentation}";
            }

            //SaveButtonVisible = !(CurrentPixPaylod.Id > 0) && CurrentPixPaylod?.PixCob != null && CurrentPixPaylod.PixCob.Validation.HasValue;

            IsActionVisible = false;

            LoadHelpPhrase();
        }

        private void LoadHelpPhrase()
        {
            var paymentType = CurrentPaylodBase.Type == PayloadType.Crypto ? "Cripto" : "Pix";

            CurrentInfo.HelpPhrase =
                $"O pagador precisa abir o app que vai fazer a transferência {paymentType} e escanear este QR Code ou colar o código copia e cola.";
        }

        private async Task Save()
        {
            var identity = await DialogService.PromptAsync(new Acr.UserDialogs.PromptConfig
            {
                Title = "Identificador",
                Message = "Digite um texto para identificar esta cobrança",
                CancelText = "Cancelar",
                OkText = "Salvar"
            });

            if (!identity.Ok)
                return;

            if (string.IsNullOrEmpty(identity?.Text))
            {
                DialogService.Toast("Ops! É preciso digitar um identificador para salvar");
                return;
            }

            CurrentPixPaylod.Identity = identity.Text;

            var success = _pixPayloadService.Save(CurrentPixPaylod);

            if (success)
            {
                await DashboardVM.LoadBilling();
            }

            else
            {
                DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app.");
            }
        }

        private void ChangeIsActionVisible()
        {
            IsActionVisible = !IsActionVisible;
        }

        private PixPayload _currentPixPaylod;
        public PixPayload CurrentPixPaylod
        {
            set => SetProperty(ref _currentPixPaylod, value);
            get => _currentPixPaylod;
        }

        private CryptoPayload _currentCryptoPaylod;
        public CryptoPayload CurrentCryptoPaylod
        {
            set => SetProperty(ref _currentCryptoPaylod, value);
            get => _currentCryptoPaylod;
        }

        private PayloadBase _currentPaylodBase;
        public PayloadBase CurrentPaylodBase
        {
            set => SetProperty(ref _currentPaylodBase, value);
            get => _currentPaylodBase;
        }

        private bool _isActionVisible;
        public bool IsActionVisible
        {
            set => SetProperty(ref _isActionVisible, value);
            get => _isActionVisible;
        }

        private PaymentViewModelInfo _currentInfo;
        public PaymentViewModelInfo CurrentInfo
        {
            set => SetProperty(ref _currentInfo, value);
            get => _currentInfo;
        }
    }

    public class PaymentViewModelInfo : NotifyObjectBase
    {
        private string _helpPhrase;
        public string HelpPhrase
        {
            set => SetProperty(ref _helpPhrase, value);
            get => _helpPhrase;
        }

        private string _value;
        public string Value
        {
            set => SetProperty(ref _value, value);
            get => _value;
        }

        private string _name;
        public string Name
        {
            set => SetProperty(ref _name, value);
            get => _name;
        }

        private string _institution;
        public string Institution
        {
            set => SetProperty(ref _institution, value);
            get => _institution;
        }

        private string _key;
        public string Key
        {
            set => SetProperty(ref _key, value);
            get => _key;
        }

        private MaterialColor _color;
        public MaterialColor Color
        {
            set => SetProperty(ref _color, value);
            get => _color;
        }
    }
}
