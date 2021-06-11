using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Style;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class AddPixKeyViewModel : BaseViewModel
    {
        

        public DashboardViewModel DashboardViewModel { get; set; }

        public AddPixKeyViewModel(DashboardViewModel dbViewModel, PixKey pixKey = null)
        {
            CurrentPixKey = pixKey ?? new PixKey();

            DashboardViewModel = dbViewModel;

            LoadData.Execute(null);
        }

        public ICommand LoadData => new Command(async () =>
        {
            await ResetProps();
        });

        private async Task ResetProps()
        {
            try
            {
                IsEdit = CurrentPixKey.Id > 0;

                CurrenSelectedFinancialInstitutionText = !IsEdit ? "Toque para selecionar" : CurrentPixKey?.FinancialInstitution?.Name;

                if (!IsEdit)
                {
                    var firstKey = _pixKeyService.GetFirst();

                    if (firstKey != null && firstKey.Id > 0)
                    {
                        CurrentPixKey.Name = firstKey?.Name;
                        CurrentPixKey.City = firstKey?.City;
                    }
                }

                ReloadStatusBar();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        public ICommand SaveCommand => new Command(async () =>
        {
            try
            {
                if (!await ValidateSave())
                    return;

                //CurrentPixKey.Color = MaterialColor.GetRandom();

                if (string.IsNullOrEmpty(CurrentPixKey?.FinancialInstitution?.Name))
                {
                    CurrentPixKey.FinancialInstitution = _financialInstitutionService.Create(FinancialInstitutionType.None);
                }

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
                    DashboardViewModel.PixKeyList.Remove(CurrentPixKey);

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
                            Name = newInstitution.Text
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
            CurrentPixKey.FinancialInstitution = institution;

            CurrenSelectedFinancialInstitutionText = institution.Name;

            SetStatusFromCurrentPixColor();
        }

        public void SetStatusFromCurrentPixColor()
        {
            if (PreferenceService.ShowInList || CurrentPixKey?.FinancialInstitution?.Institution?.Color == null)
                return;

            App.LoadTheme(CurrentPixKey?.FinancialInstitution?.Institution?.Color);

            ReloadStatusBar();
        }

        private void ReloadStatusBar()
        {
            _statusBarService.SetStatusBarColor(App.ThemeColors.PrimaryDark);
        }

        private async Task<bool> ValidateSave()
        {
            string msg = "";

            if (string.IsNullOrEmpty(CurrentPixKey?.Key))
                msg += "- Chave não informada\n";

            if (string.IsNullOrEmpty(CurrentPixKey?.Name))
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

        private string _currenSelectedFinancialInstitutionText;
        public string CurrenSelectedFinancialInstitutionText
        {
            set => SetProperty(ref _currenSelectedFinancialInstitutionText, value);
            get => _currenSelectedFinancialInstitutionText;
        }
    }
}
