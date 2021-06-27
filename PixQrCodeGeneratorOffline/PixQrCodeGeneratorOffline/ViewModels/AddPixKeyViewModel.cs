using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Style;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class AddPixKeyViewModel : BaseViewModel
    {
        public AddPixKeyViewModel(PixKey pixKey = null)
        {
            CurrentPixKey = pixKey ?? new PixKey();

            LoadData.Execute(null);
        }

        public ICommand LoadData => new Command(async () =>
        {
            await LoadInputList();

            await ResetProps();

            await LoadNotices();
        });

        private async Task ResetProps()
        {
            try
            {
                IsEdit = CurrentPixKey.Id > 0;

                SelectedFinancialInstitution = !IsEdit ? _financialInstitutionService.Create(FinancialInstitutionType.None) : CurrentPixKey.FinancialInstitution;

                //CurrenSelectedFinancialInstitutionText = !IsEdit ? "Toque para selecionar" : CurrentPixKey?.FinancialInstitution?.Name;

                CurrenKeyPlaceholder = CurrenKeyPlaceholderDefaultValue;

                var indexName = InputList.IndexOf(InputList.FirstOrDefault(x => x.Type == AddPixInputType.Name));
                var indexCity = InputList.IndexOf(InputList.FirstOrDefault(x => x.Type == AddPixInputType.City));
                var indexKey = InputList.IndexOf(InputList.FirstOrDefault(x => x.Type == AddPixInputType.Key));

                if (!IsEdit)
                {
                    var firstKey = _pixKeyService.GetFirst();

                    if (firstKey != null && firstKey.Id > 0)
                    {
                        InputList[indexName].Value = firstKey.Name;
                        InputList[indexCity].Value = firstKey.City;

                        //CurrentPixKey.Name = firstKey?.Name;
                        //CurrentPixKey.City = firstKey?.City;
                    }
                }

                else
                {
                    InputList[indexName].Value = CurrentPixKey.Name;
                    InputList[indexCity].Value = CurrentPixKey.City;
                    InputList[indexKey].Value = CurrentPixKey.Key;
                }

                SetStatusFromCurrentPixColor();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        private async Task LoadNotices()
        {
            Notices = new ObservableCollection<string>
            {
                "Para sua segurança não será possível ver saldo ou realizar transferências, use o app da própria instituição para isso.",
                "Os chaves serão guardadas somente no device, sem a necessidade de conexão com a internet e de modo criptografado.",
                "Não cadastre chaves que ainda não foram registradas em alguma instituição financeira."
            };
        }

        private async Task LoadInputList()
        {
            InputList = AddPixInput.GetList();
            InputPhasesCount = InputList?.Count - 1 ?? 0;
        }

        public ICommand SaveCommand => new Command(async () =>
        {
            try
            {

                CurrentPixKey.City = CurrentInputValues?.City;
                CurrentPixKey.Key = CurrentInputValues?.Key;
                CurrentPixKey.Name = CurrentInputValues?.Name;

                if (!await ValidateSave())
                    return;

                //CurrentPixKey.Color = MaterialColor.GetRandom();


                CurrentPixKey.FinancialInstitution = SelectedFinancialInstitution;

                if (string.IsNullOrEmpty(CurrentPixKey?.City))
                    CurrentPixKey.City = "Cidade";

                var success = false;

                //CurrentPixKey.RaisePresentation();

                if (IsEdit)
                {
                    success = _pixKeyService.Update(CurrentPixKey);

                    var l = DashboardVM.PixKeyList.FirstOrDefault(x => x.Id.Equals(CurrentPixKey.Id));

                    if (l != null)
                    {
                        int index = DashboardVM.PixKeyList.IndexOf(l);

                        if (index != -1)
                            DashboardVM.PixKeyList[index] = CurrentPixKey;
                    }
                }

                else
                {
                    success = _pixKeyService.Insert(CurrentPixKey);
                    DashboardVM.PixKeyList.Add(CurrentPixKey);
                }

                //DashboardViewModel.LoadDataCommand.Execute(null);

                //if (success)
                //{

                //CurrentPixKey.RaiseCob();

                await DashboardVM.LoadCurrentPixKey(CurrentPixKey);

                DialogService.Toast("Chave salva com sucesso");

                NavigateBack();

                //}

                //else
                //    DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app");
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        });

        public ICommand DeleteCommand => new Command(async () =>
        {
            try
            {
                var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir a chave " + CurrentPixKey.Key + "?", "Confirmação", "Sim", "Cancelar");

                if (!confirm)
                    return;

                var success = _pixKeyService.Remove(CurrentPixKey);

                if (success)
                {
                    int index = DashboardVM.PixKeyList.IndexOf(DashboardVM.PixKeyList.FirstOrDefault(x => x.Id == CurrentPixKey.Id));

                    if (index != -1)
                        DashboardVM.PixKeyList.RemoveAt(index);

                    //DashboardViewModel.PixKeyList.Remove(CurrentPixKey);

                    await DashboardVM.LoadCurrentPixKey(null);

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
        });

        public Command<AddPixInput> SelectedInstitutionCommand => new Command<AddPixInput>((input) =>
        {
            try
            {
                var options = new List<Acr.UserDialogs.ActionSheetOption>();

                var intitutionList = _financialInstitutionService.GetList();

                foreach (var item in intitutionList)
                {
                    options.Add(new Acr.UserDialogs.ActionSheetOption(item.Name, () =>
                    {
                        SetNewInstitution(item, input);
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

                        input.Title = institution?.Name;

                        SetNewInstitution(institution, input);
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

                ActualInputNextPosition = ActualInputNextPosition == 0 ? 1 : ActualInputNextPosition++;
                //CurrentPhase = ActualInputNextPosition + 1;
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        });

        public ICommand CurrentInputChangedCommand => new Command(() => CurrentInputChanged());

        private void CurrentInputChanged()
        {
            var textKey = InputList?.FirstOrDefault(x => x.Type == AddPixInputType.Key)?.Value;

            if (CurrentInput.Type == AddPixInputType.Name && string.IsNullOrEmpty(textKey))
            {
                //ActualInputNextPosition = ActualInputNextPosition--;
            }

            ShowSaveButton = CurrentInput == LastInput;
        }

        private void SetNewInstitution(FinancialInstitution institution, AddPixInput input)
        {

            input.Placeholder = institution.Name;
            input.Value = institution.Name;
            var index = InputList.IndexOf(InputList.FirstOrDefault(x => x.IsInstitution));
            InputList[index] = input;

            SelectedFinancialInstitution = institution;

            //CurrentPixKey.FinancialInstitution = institution;

            //CurrenSelectedFinancialInstitutionText = institution.Name;

            CurrenKeyPlaceholder = CurrenKeyPlaceholderDefaultValue + " no " + SelectedFinancialInstitution?.Name;

            //ActualInputNextPosition = ActualInputNextPosition;

            ActualInputNextPosition = 1;

            SetStatusFromCurrentPixColor();
        }

        public void SetStatusFromCurrentPixColor()
        {
            if (SelectedFinancialInstitution?.Institution?.MaterialColor == null)
                return;

            CurrenStyle = SelectedFinancialInstitution?.Institution?.MaterialColor;

            //App.LoadTheme(CurrentPixKey?.FinancialInstitution?.Institution?.MaterialColor);

            _statusBarService.SetStatusBarColor(SelectedFinancialInstitution.Institution.MaterialColor.Primary);
        }

        private async Task<bool> ValidateSave()
        {
            string msg = "";

            if (!CurrentPixKey.Validation.HasKey)
                msg += "- Chave não informada\n";

            if (!CurrentPixKey.Validation.HasName)
                msg += "- Nome não informado\n";

            //if (string.IsNullOrEmpty(CurrentPixKey?.City))
            //    msg += "- Cidade não informada\n";

            if (!string.IsNullOrEmpty(msg))
            {
                await DialogService.AlertAsync(msg, "Ops! Parece que faltou algo");
                return false;
            }

            return true;
        }

        public void BackButtonPressed()
        {
            DashboardVM.SetStatusFromCurrentPixColor();
        }

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

        //public static PixKey OriginPixKey { get; set; }

        //private string _currenSelectedFinancialInstitutionText;
        //public string CurrenSelectedFinancialInstitutionText
        //{
        //    set => SetProperty(ref _currenSelectedFinancialInstitutionText, value);
        //    get => _currenSelectedFinancialInstitutionText;
        //}

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

        private FinancialInstitution _selectedFinancialInstitution;
        public FinancialInstitution SelectedFinancialInstitution
        {
            set => SetProperty(ref _selectedFinancialInstitution, value);
            get => _selectedFinancialInstitution;
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
    }

    public class AddPixInput
    {
        public string Title { get; set; }

        public string Value { get; set; }

        public string Placeholder { get; set; }

        public string Icon { get; set; }

        public string Notice { get; set; }

        public AddPixInputType Type { get; set; }

        public ReturnType ReturnType { get; set; }

        public bool IsInstitution => Type == AddPixInputType.Institution;

        public static ObservableCollection<AddPixInput> GetList()
        {
            return new ObservableCollection<AddPixInput>
            {
                new AddPixInput
                {
                    Type = AddPixInputType.Institution,
                    Title = "Instituição",
                    Icon = FontAwesomeSolid.University,
                    Placeholder = "Selecione a Instituição",
                    ReturnType = ReturnType.Next,
                },
                new AddPixInput
                {
                    Type = AddPixInputType.Key,
                    Title = "Chave",
                    Icon = FontAwesomeSolid.Key,
                    Placeholder = "Digite a chave",
                    ReturnType = ReturnType.Next,
                },
                new AddPixInput
                {
                    Type = AddPixInputType.Name,
                    Title = "Nome",
                    Icon = FontAwesomeSolid.User,
                    Placeholder = "Digite o nome",
                    ReturnType = ReturnType.Next,
                },
                new AddPixInput
                {
                    Type = AddPixInputType.City,
                    Title = "Cidade",
                    Icon = FontAwesomeSolid.MapMarkedAlt,
                    Placeholder = "Digite a cidade",
                    ReturnType = ReturnType.Next,
                }
            };
        }

        //public AddPixInput Create(AddPixInputType type)
        //{
        //    switch (type)
        //    {
        //        case AddPixInputType.Institution:
        //            return new AddPixInput
        //            {
        //                Type = type,
        //                Title = "Nome"
        //            };
        //        case AddPixInputType.Key:
        //            return new AddPixInput
        //            {
        //                Type = type,
        //                Title = "Nome"
        //            };
        //        case AddPixInputType.Name:
        //            return new AddPixInput
        //            {
        //                Type = type,
        //                Title = "Nome"
        //            };
        //        case AddPixInputType.City:
        //            return new AddPixInput
        //            {
        //                Type = type,
        //                Title = "Nome"
        //            };
        //        default:
        //            return new AddPixInput();
        //    }
        //}
    }

    public class InputValues
    {
        public InputValues(ObservableCollection<AddPixInput> inputList)
        {
            InputList = inputList;
        }

        public ObservableCollection<AddPixInput> InputList { get; set; }

        public string Institution => InputList?.FirstOrDefault(x => x.Type == AddPixInputType.Institution)?.Value;

        public string Key => InputList?.FirstOrDefault(x => x.Type == AddPixInputType.Key)?.Value;

        public string Name => InputList?.FirstOrDefault(x => x.Type == AddPixInputType.Name)?.Value;

        public string City => InputList?.FirstOrDefault(x => x.Type == AddPixInputType.City)?.Value;
    }

    public enum AddPixInputType
    {
        Institution,
        Key,
        Name,
        City,
    }
}
