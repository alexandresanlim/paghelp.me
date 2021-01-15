using PixQrCodeGeneratorOffline.DataBase;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Style;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using PixQrCodeGeneratorOffline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class AddPixKeyViewModel : BaseViewModel
    {
        public IStatusBar StatusBar => DependencyService.Get<IStatusBar>();

        public DashboardViewModel DashboardViewModel { get; set; }

        public AddPixKeyViewModel(DashboardViewModel dbViewModel, PixKey pixKey = null)
        {
            CurrentPixKey = pixKey ?? new PixKey();

            if (pixKey != null)
            {
                OriginPixKey = new PixKey
                {
                    City = pixKey?.City,
                    Color = pixKey?.Color,
                    Description = pixKey?.Description,
                    FinancialInstitution = pixKey?.FinancialInstitution,
                    Id = pixKey.Id,
                    Key = pixKey?.Key,
                    Name = pixKey?.Name,
                    Payload = pixKey?.Payload,
                };
            }

            DashboardViewModel = dbViewModel;

            LoadData.Execute(null);
        }

        public ICommand LoadData => new Command(async () =>
        {
            await ResetProps();
        });

        private async Task ResetProps()
        {
            IsEdit = CurrentPixKey.Id > 0;

            CurrenSelectedFinancialInstitutionText = !IsEdit ? "Toque para selecionar" : CurrentPixKey?.FinancialInstitution?.Name;

            if (!IsEdit)
            {
                var firstKey = PixKeyDataBase.GetFirst();

                if (firstKey != null && firstKey.Id > 0)
                {
                    CurrentPixKey.Name = firstKey?.Name;
                    CurrentPixKey.City = firstKey?.City;
                }
            }

            ReloadStatusBar();
        }

        public ICommand SaveCommand => new Command(async () =>
        {
            if (!await ValidateSave())
                return;

            //CurrentPixKey.Color = MaterialColor.GetRandom();

            if (string.IsNullOrEmpty(CurrentPixKey?.FinancialInstitution?.Name))
            {
                CurrentPixKey.FinancialInstitution = new FinancialInstitution
                {
                    Name = "Não informado"
                };
                CurrentPixKey.Color = MaterialColor.GetRandom();
            }

            var success = false;

            CurrentPixKey.RaisePresentation();

            if (IsEdit)
            {
                success = PixKeyDataBase.Update(CurrentPixKey);

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
                success = PixKeyDataBase.Insert(CurrentPixKey);
                //CurrentPixKey.RaisePresentation();
                //var positionInsert = (DashboardViewModel.PixKeyList.Count - 1);
                //var last = DashboardViewModel.PixKeyList.IndexOf(DashboardViewModel.PixKeyList.LastOrDefault());
                DashboardViewModel.PixKeyList.Add(CurrentPixKey);
            }

            //DashboardViewModel.LoadDataCommand.Execute(null);

            //if (success)
            //{
            DialogService.Toast("Chave salva com sucesso");

            await NavigateBackModalAsync();

            await DashboardViewModel.LoadCurrentPixKey(CurrentPixKey);
            //}

            //else
            //    DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app");
        });

        public ICommand DeleteCommand => new Command(async () =>
        {
            var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir a chave " + CurrentPixKey.Key + "?", "Confirmação", "Sim", "Cancelar");

            if (!confirm)
                return;

            var success = PixKeyDataBase.Remove(CurrentPixKey);

            if (success)
            {
                DialogService.Toast("Chave removida com sucesso");

                await DashboardViewModel.LoadCurrentPixKey(null);

                await NavigateBackModalAsync();

                await Task.Delay(1000);

                DashboardViewModel.PixKeyList.Remove(CurrentPixKey);
            }

            else
                DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app");
        });

        public ICommand SelectedInstitutionCommand => new Command(() =>
        {
            var options = new List<Acr.UserDialogs.ActionSheetOption>();

            var intitutionList = FinancialInstitution.GetList();

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
                        Style = MaterialColor.GetRandom()
                    };

                    SetNewInstitution(institution);
                }),
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        });

        private void SetNewInstitution(FinancialInstitution institution)
        {
            CurrentPixKey.FinancialInstitution = institution;
            CurrentPixKey.Color = institution.Style;
            CurrenSelectedFinancialInstitutionText = institution.Name;
            App.LoadTheme(CurrentPixKey.Color);
            ReloadStatusBar();
        }

        private void ReloadStatusBar()
        {
            StatusBar.SetStatusBarColor(App.ThemeColors.PrimaryDark);
        }

        private async Task<bool> ValidateSave()
        {
            string msg = "";

            if (string.IsNullOrEmpty(CurrentPixKey?.Key))
                msg += "- Chave não informada\n";

            if (string.IsNullOrEmpty(CurrentPixKey?.Name))
                msg += "- Nome não informado\n";

            if (string.IsNullOrEmpty(CurrentPixKey?.City))
                msg += "- Cidade não informada\n";

            //if (string.IsNullOrEmpty(CurrentPixKey?.FinancialInstitution?.Name))
            //    msg += "- Selecione uma instituição financeira\n";

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

        public static PixKey OriginPixKey { get; set; }

        private string _currenSelectedFinancialInstitutionText;
        public string CurrenSelectedFinancialInstitutionText
        {
            set => SetProperty(ref _currenSelectedFinancialInstitutionText, value);
            get => _currenSelectedFinancialInstitutionText;
        }
    }
}
