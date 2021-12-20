using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.ViewModels.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PixQrCodeGeneratorOffline.ViewModels.PaymentMethods.Crypto
{
    public class AddCryptoKeyViewModel : ViewModelBase
    {
        public IAsyncCommand LoadDataCommand => new AsyncCommand(LoadData);

        public AddCryptoKeyViewModel(CryptoKey pixKey = null, bool isContact = false)
        {
            CurrentCryptoKey = pixKey ?? new CryptoKey();
            CurrentCryptoKey.IsContact = isContact;

            CurrentDashboard = DashboardVM; //CurrentPixKey.IsContact ? DashboardContactVM : (DashboardViewModelBase)DashboardVM;

            LoadDataCommand.ExecuteAsync().SafeFireAndForget();
        }

        private async Task LoadData()
        {
            await LoadInputList();

            await ResetProps();
        }

        private async Task ResetProps()
        {
            try
            {
                IsEdit = CurrentCryptoKey.Id > 0;

                SelectedFinancialInstitution = !IsEdit ? _financialInstitutionCryptoService.Create(FinancialInstitutionCryptoType.None) : CurrentCryptoKey.FinancialInstitution;

                CurrenKeyPlaceholder = CurrenKeyPlaceholderDefaultValue;

                if (!IsEdit)
                {
                    if (!CurrentCryptoKey.IsContact)
                    {
                        if (CurrentInputValues.Institution.Index > -1)
                        {
                            InputList[CurrentInputValues.Institution.Index].Placeholder = "Toque para selecionar";
                        }

                        var firstKey = _pixKeyService.GetAll().LastOrDefault();

                        if (firstKey != null && firstKey.Id > 0)
                        {
                            InputList[CurrentInputValues.Name.Index].Value = firstKey.Name;
                            InputList[CurrentInputValues.City.Index].Value = firstKey.City;
                        }
                    }

                    if (Clipboard.HasText)
                    {
                        var text = await Clipboard.GetTextAsync();

                        if (text.IsAKey())
                        {
                            InputList[CurrentInputValues.Key.Index].Value = text;
                        }
                    }
                }

                else
                {
                    if (CurrentInputValues.Institution.Index > -1)
                        InputList[CurrentInputValues.Institution.Index].Placeholder = SelectedFinancialInstitution.Name;

                    InputList[CurrentInputValues.Key.Index].Value = CurrentCryptoKey.Key;
                }
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        private async Task LoadInputList()
        {
            InputList = AddPixInput.GetList(false);
            InputPhasesCount = InputList?.Count - 1 ?? 0;
        }

        private string CurrenKeyPlaceholderDefaultValue => "Chave pública ";

        public FinancialInstitutionCrypto SelectedFinancialInstitution { get; set; }

        public DashboardViewModel CurrentDashboard { get; set; }

        private InputValues CurrentInputValues => new InputValues(InputList);

        private int _inputPhasesCount;
        public int InputPhasesCount
        {
            set => SetProperty(ref _inputPhasesCount, value);
            get => _inputPhasesCount;
        }

        private string _currenKeyPlaceholder;
        public string CurrenKeyPlaceholder
        {
            set => SetProperty(ref _currenKeyPlaceholder, value);
            get => _currenKeyPlaceholder;
        }

        private bool _isEdit;
        public bool IsEdit
        {
            set => SetProperty(ref _isEdit, value);
            get => _isEdit;
        }

        private ObservableCollection<AddPixInput> _inputList;
        public ObservableCollection<AddPixInput> InputList
        {
            set => SetProperty(ref _inputList, value);
            get => _inputList;
        }

        private CryptoKey _currentCrytoKey;
        public CryptoKey CurrentCryptoKey
        {
            set => SetProperty(ref _currentCrytoKey, value);
            get => _currentCrytoKey;
        }
    }
}
