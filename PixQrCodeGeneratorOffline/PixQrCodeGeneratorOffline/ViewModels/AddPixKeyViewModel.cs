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
        public DashboardViewModel DashboardViewModel { get; set; }

        public AddPixKeyViewModel(DashboardViewModel dashboardVM, PixKey pixKey = null)
        {
            DashboardViewModel = dashboardVM;

            CurrentPixKey = pixKey ?? new PixKey();

            LoadData.Execute(null);
        }

        public ICommand LoadData => new Command(async () =>
        {
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

                if (!IsEdit)
                {
                    var firstKey = _pixKeyService.GetFirst();

                    if (firstKey != null && firstKey.Id > 0)
                    {
                        CurrentPixKey.Name = firstKey?.Name;
                        CurrentPixKey.City = firstKey?.City;
                    }
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

        public ICommand SaveCommand => new Command(async () =>
        {
            try
            {
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

                    var l = DashboardViewModel.PixKeyList.FirstOrDefault(x => x.Id.Equals(CurrentPixKey.Id));

                    if (l != null)
                    {
                        int index = DashboardViewModel.PixKeyList.IndexOf(l);

                        if (index != -1)
                            DashboardViewModel.PixKeyList[index] = CurrentPixKey;
                    }
                }

                else
                {
                    success = _pixKeyService.Insert(CurrentPixKey);
                    DashboardViewModel.PixKeyList.Add(CurrentPixKey);
                }

                //DashboardViewModel.LoadDataCommand.Execute(null);

                //if (success)
                //{

                //CurrentPixKey.RaiseCob();

                await DashboardViewModel.LoadCurrentPixKey(CurrentPixKey);

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
                    int index = DashboardViewModel.PixKeyList.IndexOf(DashboardViewModel.PixKeyList.FirstOrDefault(x => x.Id == CurrentPixKey.Id));

                    if (index != -1)
                        DashboardViewModel.PixKeyList.RemoveAt(index);

                    //DashboardViewModel.PixKeyList.Remove(CurrentPixKey);

                    await DashboardViewModel.LoadCurrentPixKey(null);

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

        private void SetNewInstitution(FinancialInstitution institution)
        {
            SelectedFinancialInstitution = institution;

            //CurrentPixKey.FinancialInstitution = institution;

            //CurrenSelectedFinancialInstitutionText = institution.Name;

            CurrenKeyPlaceholder = CurrenKeyPlaceholderDefaultValue + " no " + SelectedFinancialInstitution?.Name;

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

        private string CurrenKeyPlaceholderDefaultValue => "Chave cadastrada";
    }
}
