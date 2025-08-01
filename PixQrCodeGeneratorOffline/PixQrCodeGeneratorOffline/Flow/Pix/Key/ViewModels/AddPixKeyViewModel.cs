﻿using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Helpers;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
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

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class AddPixKeyViewModel : ViewModelBase
    {
        #region Commands

        public IAsyncCommand LoadDataCommand => new AsyncCommand(LoadData);

        public IAsyncCommand SaveCommand => new AsyncCommand(Save);

        public ICommand SelectedInstitutionCommand => new Command(SelectedInstitution);

        public ICommand InputNextCommand => new Command(InputNext);

        public ICommand CurrentInputChangedCommand => new Command(CurrentInputChanged);

        #endregion

        public AddPixKeyViewModel(PixKey pixKey = null, bool isContact = false)
        {
            CurrentPixKey = pixKey ?? new PixKey();
            CurrentPixKey.IsContact = isContact;

            CurrentDashboard = DashboardVM;
            CurrentContactDashboard = DashboardContactVM;

            LoadDataCommand.ExecuteAsync().SafeFireAndForget();
        }

        private async Task LoadData()
        {
            LoadInputList();

            await ResetProps();
        }

        private async Task ResetProps()
        {
            try
            {
                _isEdit = CurrentPixKey?.Id > 0;

                SelectedFinancialInstitution = !_isEdit ? _financialInstitutionService.Create(FinancialInstitutionType.None) : CurrentPixKey.FinancialInstitution;

                CurrenKeyPlaceholder = CurrenKeyPlaceholderDefaultValue;

                if (!_isEdit)
                {
                    if (!CurrentPixKey.IsContact)
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
                            var exists = _pixKeyService.GetAll(x => x.Key == text);

                            if (exists.IsNullOrEmpty())
                                InputList[CurrentInputValues.Key.Index].Value = text;
                        }
                    }
                }

                else
                {
                    if (CurrentInputValues.Institution.Index > -1)
                        InputList[CurrentInputValues.Institution.Index].Placeholder = SelectedFinancialInstitution.Name;

                    InputList[CurrentInputValues.Name.Index].Value = CurrentPixKey.Name;
                    InputList[CurrentInputValues.City.Index].Value = CurrentPixKey.City;
                    InputList[CurrentInputValues.Key.Index].Value = CurrentPixKey.Key;
                }
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        private void LoadInputList()
        {
            InputList = AddPixInput.GetList(CurrentPixKey.IsContact);
            InputPhasesCount = InputList?.Count - 1 ?? 0;
        }

        private async Task Save()
        {
            CurrentPixKey.City = CurrentInputValues?.City.Value;
            CurrentPixKey.Key = CurrentInputValues?.Key?.Value;
            CurrentPixKey.Name = CurrentInputValues?.Name?.Value;
            CurrentPixKey.FinancialInstitution = SelectedFinancialInstitution;

            if (string.IsNullOrWhiteSpace(CurrentPixKey?.City))
                CurrentPixKey.City = Constants.INPUT_CITY_TEXT;

            if (!ValidateSave())
                return;

            try
            {
                SetIsLoading(true);

                CurrentPixKey.Viewer = _pixKeyViewerService.Create(CurrentPixKey);
                CurrentPixKey.Payload = _pixPayloadService.Create(CurrentPixKey);
                CurrentPixKey.Command = _pixKeyCommand.Create(CurrentPixKey);

                var success = false;

                if (_isEdit)
                {
                    success = _pixKeyService.Update(CurrentPixKey);

                    var l = CurrentPixKey.IsContact ? CurrentContactDashboard.PixKeyListContact.FirstOrDefault(x => x.Id.Equals(CurrentPixKey.Id)) : CurrentDashboard.PixKeyList.FirstOrDefault(x => x.Id.Equals(CurrentPixKey.Id));

                    if (l != null)
                    {
                        int index = CurrentPixKey.IsContact ? CurrentContactDashboard.PixKeyListContact.IndexOf(l) : CurrentDashboard.PixKeyList.IndexOf(l);

                        if (index != -1)
                        {
                            if (CurrentPixKey.IsContact)
                                CurrentContactDashboard.PixKeyListContact[index] = CurrentPixKey;

                            else
                                CurrentDashboard.PixKeyList[index] = CurrentPixKey;
                        }
                    }
                }

                else
                {
                    success = _pixKeyService.Insert(CurrentPixKey);

                    if (CurrentPixKey.IsContact)
                    {
                        if (CurrentContactDashboard.PixKeyListContact.Count == 0)
                            CurrentContactDashboard.PixKeyListContact = new ObservableCollection<PixKey>(new List<PixKey> { CurrentPixKey });

                        else
                            CurrentContactDashboard.PixKeyListContact.Add(CurrentPixKey);
                    }

                    else
                    {
                        if (CurrentDashboard.PixKeyList.Count == 0)
                            CurrentDashboard.PixKeyList = new ObservableCollection<PixKey>(new List<PixKey> { CurrentPixKey });

                        else
                            CurrentDashboard.PixKeyList.Add(CurrentPixKey);

                        CurrentDashboard.CurrentPixKey = CurrentPixKey;
                    }
                }

                if (success)
                {
                    DialogService.Toast("Chave salva com sucesso");
                    await NavigateBackPopupAsync().ConfigureAwait(false);
                }

                else
                {
                    DialogService.Toast(Constants.ERROR_MSG);
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

        private void SelectedInstitution()
        {
            try
            {
                var options = new List<Acr.UserDialogs.ActionSheetOption>();

                var intitutionList = _financialInstitutionService.GetList();

                foreach (var item in intitutionList)
                {
                    options.Add(new Acr.UserDialogs.ActionSheetOption(item.Name, () =>
                    {
                        SetNewInstitution(item);
                    }));
                }

                DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
                {
                    Title = "Selecione uma instituição",
                    Message = "Caso sua instituição não esteja na lista, toque em adicionar nova",
                    Options = options,

                    Destructive = new Acr.UserDialogs.ActionSheetOption("ADICIONAR NOVA", async () =>
                    {
                        var newInstitution = await DialogService.PromptAsync(new Acr.UserDialogs.PromptConfig
                        {
                            CancelText = "Cancelar",
                            InputType = Acr.UserDialogs.InputType.Name,
                            OkText = "Adicionar",
                            Title = "Instituição: ",
                            Placeholder = "Digite o nome da instituição",
                        });

                        if (!newInstitution.Ok)
                            return;

                        var institution = new FinancialInstitution
                        {
                            Name = newInstitution.Text,
                            Type = FinancialInstitutionType.None
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

        private void InputNext()
        {
            try
            {
                if (ShowSaveButton)
                    return;

                ActualInputNextPosition++;
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        private void CurrentInputChanged() => ShowSaveButton = CurrentInput == LastInput;

        private void SetNewInstitution(FinancialInstitution institution)
        {

            SelectedFinancialInstitution = institution;

            InputList[CurrentInputValues.Institution.Index].Placeholder = institution.Name;
            InputList[CurrentInputValues.Institution.Index].Value = institution.Name;
            InputList[CurrentInputValues.Institution.Index].Title = institution.Name;

            if (SelectedFinancialInstitution.Type != FinancialInstitutionType.None)
                InputList[CurrentInputValues.Key.Index].Placeholder = CurrenKeyPlaceholderDefaultValue + "no(a) " + SelectedFinancialInstitution?.Name;

            ActualInputNextPosition = 1;
        }


        private bool ValidateSave()
        {

            if (!CurrentPixKey.HasKey())
            {
                DialogService.Toast("Ops! Chave não informada");
                ActualInputNextPosition = CurrentInputValues.Key.Index;
                return false;
            }

            if (!CurrentPixKey.HasName())
            {
                ActualInputNextPosition = CurrentInputValues.Name.Index;
                DialogService.Toast("Ops! Nome não informado");
                return false;
            }

            return true;
        }

        private bool _isEdit;

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }

        private string _currenKeyPlaceholder;
        public string CurrenKeyPlaceholder
        {
            set => SetProperty(ref _currenKeyPlaceholder, value);
            get => _currenKeyPlaceholder;
        }

        private ObservableCollection<string> _notices;
        public ObservableCollection<string> Notices
        {
            set => SetProperty(ref _notices, value);
            get => _notices;
        }

        private MaterialColor _currenStyle;
        public MaterialColor CurrenStyle
        {
            set => SetProperty(ref _currenStyle, value);
            get => _currenStyle;
        }

        private ObservableCollection<AddPixInput> _inputList;
        public ObservableCollection<AddPixInput> InputList
        {
            set => SetProperty(ref _inputList, value);
            get => _inputList;
        }

        private bool _showSaveButton;
        public bool ShowSaveButton
        {
            set => SetProperty(ref _showSaveButton, value);
            get => _showSaveButton;
        }

        private AddPixInput _currentInput;
        public AddPixInput CurrentInput
        {
            set => SetProperty(ref _currentInput, value);
            get => _currentInput;
        }

        private int _actualInputNextPosition;
        public int ActualInputNextPosition
        {
            set => SetProperty(ref _actualInputNextPosition, value);
            get => _actualInputNextPosition;
        }

        private int _inputPhasesCount;
        public int InputPhasesCount
        {
            set => SetProperty(ref _inputPhasesCount, value);
            get => _inputPhasesCount;
        }

        private AddPixInput LastInput => InputList?.LastOrDefault() ?? new AddPixInput();

        private InputValues CurrentInputValues => new InputValues(InputList);

        private string CurrenKeyPlaceholderDefaultValue => "Chave Pix ";

        public FinancialInstitution SelectedFinancialInstitution { get; set; }

        public DashboardViewModel CurrentDashboard { get; set; }

        public DashboardContactViewModel CurrentContactDashboard { get; set; }
    }
}
