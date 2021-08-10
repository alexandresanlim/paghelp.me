using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Style;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using PixQrCodeGeneratorOffline.ViewModels.Base;
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
        public AddPixKeyViewModel(PixKey pixKey = null, bool isContact = false)
        {
            CurrentPixKey = pixKey ?? new PixKey();
            CurrentPixKey.IsContact = isContact;

            CurrentDashboard = DashboardVM; //CurrentPixKey.IsContact ? DashboardContactVM : (DashboardViewModelBase)DashboardVM;

            LoadData.Execute(null);
        }

        public ICommand LoadData => new Command(async () =>
        {
            await LoadInputList();

            await ResetProps();

            //await LoadNotices();
        });

        private async Task ResetProps()
        {
            try
            {
                IsEdit = CurrentPixKey.Id > 0;

                SelectedFinancialInstitution = !IsEdit ? _financialInstitutionService.Create(FinancialInstitutionType.None) : CurrentPixKey.FinancialInstitution;

                CurrenKeyPlaceholder = CurrenKeyPlaceholderDefaultValue;

                if (!IsEdit)
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

                SetStatusFromCurrentPixColor();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        //private async Task LoadNotices()
        //{
        //    Notices = new ObservableCollection<string>
        //    {
        //        "Para sua segurança não será possível ver saldo ou realizar transferências, use o app da própria instituição para isso.",
        //        "Os chaves serão guardadas somente no device, sem a necessidade de conexão com a internet e de modo criptografado.",
        //        "Não cadastre chaves que ainda não foram registradas em alguma instituição financeira."
        //    };
        //}

        private async Task LoadInputList()
        {
            InputList = AddPixInput.GetList(CurrentPixKey.IsContact);
            InputPhasesCount = InputList?.Count - 1 ?? 0;
        }

        public ICommand SaveCommand => new Command(async () =>
        {
            CurrentPixKey.City = CurrentInputValues?.City.Value;
            CurrentPixKey.Key = CurrentInputValues?.Key?.Value;
            CurrentPixKey.Name = CurrentInputValues?.Name?.Value;
            CurrentPixKey.FinancialInstitution = SelectedFinancialInstitution;

            if (!await ValidateSave())
                return;

            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                //CurrentPixKey.Color = MaterialColor.GetRandom();

                if (string.IsNullOrEmpty(CurrentPixKey?.City))
                {
                    CurrentPixKey.City = "Cidade";
                }

                //var success = false;

                //if (IsEdit)
                //{
                //    success = _pixKeyService.Update(CurrentPixKey);

                //    //var l = CurrentDashboard.PixKeyList.FirstOrDefault(x => x.Id.Equals(CurrentPixKey.Id));

                //    //if (l != null)
                //    //{
                //    //    int index = CurrentDashboard.PixKeyList.IndexOf(l);

                //    //    if (index != -1)
                //    //        CurrentDashboard.PixKeyList[index] = CurrentPixKey;
                //    //}
                //}

                //else
                //{
                //    success = _pixKeyService.Insert(CurrentPixKey);
                //    //await CurrentDashboard.LoadPixKey();

                //    //CurrentDashboard.PixKeyList.Add(CurrentPixKey);
                //}

                var success = IsEdit ? _pixKeyService.Update(CurrentPixKey) : _pixKeyService.Insert(CurrentPixKey);

                //DashboardViewModel.LoadDataCommand.Execute(null);

                //if (success)
                //{

                //CurrentPixKey.RaiseCob();

                //await CurrentDashboard.LoadCurrentPixKey(CurrentPixKey);

                //if (CurrentPixKey.IsContact)
                //    App.LoadTheme();

                if (success)
                {
                    if (CurrentPixKey.IsContact)
                        await CurrentDashboard.LoadPixKeyContact();

                    else
                        await CurrentDashboard.LoadPixKey();

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
        });

        public ICommand DeleteCommand => new Command(async () =>
        {
            var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir a chave " + CurrentPixKey.Key + "?", "Confirmação", "Sim", "Cancelar");

            if (!confirm)
                return;

            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                var success = _pixKeyService.Remove(CurrentPixKey);

                if (success)
                {
                    int index = CurrentDashboard.PixKeyList.IndexOf(CurrentDashboard.PixKeyList.FirstOrDefault(x => x.Id == CurrentPixKey.Id));

                    if (index != -1)
                        CurrentDashboard.PixKeyList.RemoveAt(index);

                    await CurrentDashboard.LoadCurrentPixKey(null);

                    DialogService.Toast("Chave removida com sucesso");

                    NavigateBack();
                }

                else
                    DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app");
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetIsLoading(false);
            }
        });

        public ICommand SelectedInstitutionCommand => new Command(() =>
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
                    Title = "Selecione um instituição",
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
        });

        public ICommand InputNextCommand => new Command(async () =>
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
        });

        public ICommand CurrentInputChangedCommand => new Command(() => CurrentInputChanged());

        private void CurrentInputChanged()
        {
            ShowSaveButton = CurrentInput == LastInput;
        }

        private void SetNewInstitution(FinancialInstitution institution)
        {

            SelectedFinancialInstitution = institution;

            InputList[CurrentInputValues.Institution.Index].Placeholder = institution.Name;
            InputList[CurrentInputValues.Institution.Index].Value = institution.Name;
            InputList[CurrentInputValues.Institution.Index].Title = institution.Name;

            if (SelectedFinancialInstitution.Type != FinancialInstitutionType.None)
                InputList[CurrentInputValues.Key.Index].Placeholder = CurrenKeyPlaceholderDefaultValue + " no(a) " + SelectedFinancialInstitution?.Name;

            ActualInputNextPosition = 1;

            SetStatusFromCurrentPixColor();
        }

        public void SetStatusFromCurrentPixColor()
        {
            return;

            if (SelectedFinancialInstitution?.Institution?.MaterialColor == null)
                return;

            CurrenStyle = SelectedFinancialInstitution?.Institution?.MaterialColor;

            //_statusBarService.SetStatusBarColor(SelectedFinancialInstitution.Institution.MaterialColor.Primary);
        }

        private async Task<bool> ValidateSave()
        {

            if (!CurrentPixKey.Validation.HasKey)
            {
                DialogService.Toast("Ops! Chave não informada");
                ActualInputNextPosition = CurrentInputValues.Key.Index;
                return false;
            }

            if (!CurrentPixKey.Validation.HasName)
            {
                ActualInputNextPosition = CurrentInputValues.Name.Index;
                DialogService.Toast("Ops! Nome não informado");
                return false;
            }

            return true;
        }

        //public void BackButtonPressed()
        //{
        //    if (!CurrentPixKey.IsContact)
        //        DashboardVM.SetStatusFromCurrentPixColor();

        //    else
        //        App.LoadTheme();
        //}

        private bool _isEdit;
        public bool IsEdit
        {
            set => SetProperty(ref _isEdit, value);
            get => _isEdit;
        }

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

        private string CurrenKeyPlaceholderDefaultValue => "Chave cadastrada";

        public FinancialInstitution SelectedFinancialInstitution { get; set; }

        public DashboardViewModel CurrentDashboard { get; set; }
    }
}
