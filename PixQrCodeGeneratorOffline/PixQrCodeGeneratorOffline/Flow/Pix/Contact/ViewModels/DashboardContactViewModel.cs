using Acr.UserDialogs;
using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Helpers;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class DashboardContactViewModel : DashboardViewModelBase
    {
        #region Commands

        public IAsyncCommand LoadDataCommand => new AsyncCommand(LoadData);

        public IAsyncCommand ExportToFileContactCommand => new AsyncCommand(async () => await _pixKeyService.ExportToFileContact(PixKeyListContact));

        public IAsyncCommand NavigateToAddNewKeyPageContactCommand => new AsyncCommand(async () => await _pixKeyService.NavigateToAdd(isContact: true));

        public IAsyncCommand RemoveAllKeyContactCommand => new AsyncCommand(RemoveAllContactKeys);

        public ICommand DeleteContactKeyCommand => new AsyncCommand<PixKey>(DeleteContactKey);

        public ICommand OpenOptionsCommand => new Command(OpenOptions);

        #endregion

        public DashboardContactViewModel()
        {
            DashboardContactVM = this;
        }

        public async Task LoadData()
        {
            try
            {
                await LoadAuthenticationPage(() =>
                {
                    LoadPixKeyContact();
                });

                //LoadCurrentPixKey();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        }

        public void LoadPixKeyContact()
        {
            PixKeyListContact = new ObservableCollection<PixKey>();

            PixKeyListContact = _pixKeyService?
                .GetAll(isContact: true)?.OrderBy(x => x?.Name)?
                .ToObservableCollection() ?? new ObservableCollection<PixKey>();

            //var piskeyListContactGrouped = PixKeyListContact.GroupBy(x => x.Viewer.StartLetter);

            //foreach (var item in piskeyListContactGrouped)
            //{
            //    PixKeyListContactGroup.Add(new PixKeyGroup(item.Key, item.ToList()));
            //}
        }

        #region Contact

        private async Task RemoveAllContactKeys()
        {
            var success = await _pixKeyService.RemoveAll(isContact: true);

            if (success)
                PixKeyListContact = new ObservableCollection<PixKey>();
        }

        private async Task DeleteContactKey(PixKey contactKey)
        {
            var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir a chave do contato " + contactKey?.Name + "?", "Confirmação", "Sim", "Cancelar");

            if (!confirm)
                return;

            try
            {
                SetIsLoading(true);

                var success = _pixKeyService.Remove(contactKey);

                if (success)
                {
                    int index = PixKeyListContact.IndexOf(PixKeyListContact.FirstOrDefault(x => x.Id == contactKey.Id));

                    if (index != -1)
                        PixKeyListContact.RemoveAt(index);

                    if (PixKeyListContact.Count == 0)
                    {
                        PixKeyListContact = new ObservableCollection<PixKey>();
                    }

                    DialogService.Toast("Chave removida com sucesso");
                }

                else
                    ShowToastErrorMessage();
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

        private void OpenOptions()
        {
            try
            {
                var options = new List<ActionSheetOption>()
                {
                    new ActionSheetOption("Adicionar nova chave",() => NavigateToAddNewKeyPageContactCommand.Execute(null)),
                    new ActionSheetOption("Exportar todas",() => ExportToFileContactCommand.Execute(null)),
                    new ActionSheetOption("Excluir todas",() => RemoveAllKeyContactCommand.Execute(null)),
                };

                DialogService.ActionSheet(new ActionSheetConfig
                {
                    Title = "Selecione uma opção",
                    Options = options,
                    Cancel = new ActionSheetOption(Constants.CANCEL, () =>
                    {
                        return;
                    })
                });
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

        #endregion

        private ObservableCollection<PixKey> _pixKeyListContact;
        public ObservableCollection<PixKey> PixKeyListContact
        {
            set => SetProperty(ref _pixKeyListContact, value);
            get => _pixKeyListContact;
        }

        //public List<PixKeyGroup> PixKeyListContactGroup { get; private set; } = new List<PixKeyGroup>();
    }

    //public static class DashboardContactViewModelExtention
    //{
    //    private static async Task LoadDataContact(this DashboardContactViewModel dashboardViewModelBase)
    //    {
    //        //var list = _pixKeyService?.GetAll(isContact: true);

    //        //dashboardViewModelBase.PixKeyList = list?.OrderBy(x => x?.FinancialInstitution?.Name)?.ToObservableCollection() ?? new ObservableCollection<PixKey>();

    //        //await dashboardViewModelBase.LoadCurrentPixKey();
    //    }
    //}
}
