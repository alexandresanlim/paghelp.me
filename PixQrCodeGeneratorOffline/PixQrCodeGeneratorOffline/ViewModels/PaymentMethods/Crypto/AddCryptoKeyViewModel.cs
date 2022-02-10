using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels.PaymentMethods.Crypto
{
    public class AddCryptoKeyViewModel : ViewModelBase
    {
        public ICommand LoadDataCommand => new Command(LoadData);

        public ICommand InputNextCommand => new Command(InputNext);

        public IAsyncCommand SaveCommand => new AsyncCommand(Save);

        public IAsyncCommand DeleteCommand => new AsyncCommand(Delete);

        public ICommand SelectedInstitutionCommand => new Command(SelectedInstitution);

        public ICommand CurrentInputChangedCommand => new Command(CurrentInputChanged);

        public AddCryptoKeyViewModel(CryptoKey pixKey = null, bool isContact = false)
        {
            CurrentCryptoKey = pixKey ?? new CryptoKey();
            CurrentCryptoKey.IsContact = isContact;

            CurrentDashboard = DashboardCryptoVM; //CurrentPixKey.IsContact ? DashboardContactVM : (DashboardViewModelBase)DashboardVM;

            LoadDataCommand.Execute(null);
        }

        private void LoadData()
        {
            LoadInputList();

            ResetProps();
        }

        private void ResetProps()
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
                    }

                    //if (Clipboard.HasText)
                    //{
                    //    var text = await Clipboard.GetTextAsync();

                    //    if (text.IsAKey())
                    //    {
                    //        var exists = _cryptoKeyService.GetAll(x => x.Key == text);

                    //        if (exists != null && exists.FirstOrDefault().Id > 0)
                    //            InputList[CurrentInputValues.Key.Index].Value = text;
                    //    }
                    //}
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

        private async Task Save()
        {
            CurrentCryptoKey.Key = CurrentInputValues?.Key?.Value;
            CurrentCryptoKey.FinancialInstitution = SelectedFinancialInstitution;

            if (!ValidateSave())
                return;

            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                var success = false;

                if (IsEdit)
                {
                    success = _cryptoKeyService.Update(CurrentCryptoKey);

                    //var l = CurrentCryptoKey.IsContact ? CurrentDashboard.PixKeyListContact.FirstOrDefault(x => x.Id.Equals(CurrentCryptoKey.Id)) : CurrentDashboard.CryptoKeyList.FirstOrDefault(x => x.Id.Equals(CurrentCryptoKey.Id));

                    var l = CurrentDashboard.CryptoKeyList.FirstOrDefault(x => x.Id.Equals(CurrentCryptoKey.Id));

                    if (l != null)
                    {
                        //int index = CurrentCryptoKey.IsContact ? CurrentDashboard.PixKeyListContact.IndexOf(l) : CurrentDashboard.CryptoKeyList.IndexOf(l);

                        int index = CurrentDashboard.CryptoKeyList.IndexOf(l);

                        //Atualiza dashboard

                        if (index != -1)
                        {
                            if (CurrentCryptoKey.IsContact)
                            {   //CurrentDashboard.PixKeyListContact[index] = CurrentCryptoKey;
                            }


                            else
                                CurrentDashboard.CryptoKeyList[index] = CurrentCryptoKey;
                        }
                    }
                }

                else
                {
                    success = _cryptoKeyService.Insert(CurrentCryptoKey);

                    // Atualiza dashboard

                    if (CurrentCryptoKey.IsContact)
                    {
                        //if (CurrentDashboard.PixKeyListContact.Count == 0)
                        //{
                        //    CurrentDashboard.PixKeyListContact = new ObservableCollection<CryptoKey>
                        //    {
                        //        CurrentCryptoKey
                        //    };
                        //}

                        //else
                        //    CurrentDashboard.PixKeyListContact.Add(CurrentPixKey);
                    }

                    else
                    {
                        if (CurrentDashboard.CryptoKeyList.Count == 0)
                        {
                            CurrentDashboard.CryptoKeyList = new ObservableCollection<CryptoKey>
                            {
                                CurrentCryptoKey
                            };
                        }

                        else
                        {
                            CurrentDashboard.CryptoKeyList.Add(CurrentCryptoKey);
                        }

                        CurrentDashboard.CurrentCryptoKey = CurrentCryptoKey;
                    }
                }

                if (success)
                {
                    DialogService.Toast("Chave salva com sucesso");
                    NavigateBack();
                }

                else
                {
                    DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app");
                }
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetIsLoading(false);
            }
        }

        private async Task Delete()
        {
            var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir a chave " + CurrentCryptoKey.Key + "?", "Confirmação", "Sim", "Cancelar");

            if (!confirm)
                return;

            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                var success = _cryptoKeyService.Remove(CurrentCryptoKey);

                if (success)
                {
                    //int index = CurrentCryptoKey.IsContact ?
                    //CurrentDashboard.CryptoKeyListContact.IndexOf(CurrentDashboard.PixKeyListContact.FirstOrDefault(x => x.Id == CurrentPixKey.Id)) :
                    //CurrentDashboard.PixKeyList.IndexOf(CurrentDashboard.PixKeyList.FirstOrDefault(x => x.Id == CurrentPixKey.Id));

                    var index = CurrentDashboard.CryptoKeyList.IndexOf(CurrentDashboard.CryptoKeyList.FirstOrDefault(x => x.Id == CurrentCryptoKey.Id));

                    if (index != -1)
                    {
                        if (CurrentCryptoKey.IsContact)
                        {
                            //CurrentDashboard.CryptoKeyListContact.RemoveAt(index);
                        }

                        else
                        {
                            CurrentDashboard.CryptoKeyList.RemoveAt(index);
                            CurrentDashboard.CurrentCryptoKey = CurrentDashboard?.CryptoKeyList?.FirstOrDefault() ?? new CryptoKey();
                        }
                    }

                    if (CurrentDashboard.CryptoKeyList.Count == 0)
                    {
                        //CurrentDashboard.CryptoKeyList.Clear();
                        CurrentDashboard.CryptoKeyList = new ObservableCollection<CryptoKey>();
                    }

                    //if (CurrentDashboard.PixKeyListContact.Count == 0)
                    //{
                    //    CurrentDashboard.PixKeyListContact = new ObservableCollection<CryptoKey>();
                    //}

                    DialogService.Toast("Chave removida com sucesso");

                    NavigateBack();
                }

                else
                {
                    DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app");
                }
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetIsLoading(false);
            }

        }

        private bool ValidateSave()
        {
            if (!CurrentCryptoKey.Validation.HasKey)
            {
                DialogService.Toast("Ops! Chave não informada");
                ActualInputNextPosition = CurrentInputValues.Key.Index;
                return false;
            }

            return true;
        }

        private void InputNext()
        {
            try
            {
                if (ShowSaveButton)
                    return;

                ActualInputNextPosition++;
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        }

        private void CurrentInputChanged()
        {
            ShowSaveButton = CurrentInput == LastInput;
        }

        private void SelectedInstitution()
        {
            try
            {
                var options = new List<Acr.UserDialogs.ActionSheetOption>();

                var intitutionList = _financialInstitutionCryptoService.GetList();

                foreach (var item in intitutionList)
                {
                    options.Add(new Acr.UserDialogs.ActionSheetOption(item.Name, () =>
                    {
                        SetNewInstitution(item);
                    }));
                }

                DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
                {
                    Title = "Selecione uma cripto",
                    Message = "Caso sua instituição não esteja na lista, toque em adicionar nova",
                    Options = options,
                    //UseBottomSheet = true,
                    Destructive = new Acr.UserDialogs.ActionSheetOption("ADICIONAR NOVA", async () =>
                    {
                        var newInstitution = await DialogService.PromptAsync(new Acr.UserDialogs.PromptConfig
                        {
                            CancelText = "Cancelar",
                            InputType = Acr.UserDialogs.InputType.Name,
                            OkText = "Adicionar",
                            Title = "Criptomoeda: ",
                            Placeholder = "Digite o nome da criptomoeda",
                        });

                        if (!newInstitution.Ok)
                            return;

                        var institution = new FinancialInstitutionCrypto
                        {
                            Name = newInstitution.Text,
                            Type = FinancialInstitutionCryptoType.None
                        };

                        SetNewInstitution(institution);
                    }),
                    Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                    {
                        return;
                    })
                });
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        private void SetNewInstitution(FinancialInstitutionCrypto institution)
        {

            SelectedFinancialInstitution = institution;

            InputList[CurrentInputValues.Institution.Index].Placeholder = institution.Name;
            InputList[CurrentInputValues.Institution.Index].Value = institution.Name;
            InputList[CurrentInputValues.Institution.Index].Title = institution.Name;

            if (SelectedFinancialInstitution.Type != FinancialInstitutionCryptoType.None)
                InputList[CurrentInputValues.Key.Index].Placeholder = CurrenKeyPlaceholderDefaultValue + " " + SelectedFinancialInstitution?.Name;

            ActualInputNextPosition = 1;
        }

        private void LoadInputList()
        {
            InputList = AddCryptoInput.GetList(false);
            InputPhasesCount = InputList?.Count - 1 ?? 0;
        }

        private string CurrenKeyPlaceholderDefaultValue => "Chave pública ";

        public FinancialInstitutionCrypto SelectedFinancialInstitution { get; set; }

        public DashboardCryptoViewModel CurrentDashboard { get; set; }

        private InputCryptoValues CurrentInputValues => new InputCryptoValues(InputList);

        private AddCryptoInput LastInput => InputList?.LastOrDefault() ?? new AddCryptoInput();

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

        private int _actualInputNextPosition;
        public int ActualInputNextPosition
        {
            set => SetProperty(ref _actualInputNextPosition, value);
            get => _actualInputNextPosition;
        }

        private bool _showSaveButton;
        public bool ShowSaveButton
        {
            set => SetProperty(ref _showSaveButton, value);
            get => _showSaveButton;
        }

        private AddCryptoInput _currentInput;
        public AddCryptoInput CurrentInput
        {
            set => SetProperty(ref _currentInput, value);
            get => _currentInput;
        }

        private ObservableCollection<AddCryptoInput> _inputList;
        public ObservableCollection<AddCryptoInput> InputList
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
