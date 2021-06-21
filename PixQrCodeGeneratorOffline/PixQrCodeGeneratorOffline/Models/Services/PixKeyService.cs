using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using PixQrCodeGeneratorOffline.ViewModels;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixKeyService : IPixKeyService
    {
        private IUserDialogs DialogService => UserDialogs.Instance;

        private readonly IPixKeyRepository _pixKeyRepository;

        private readonly IExternalActionService _externalActionService;

        private readonly IEventService _eventService;

        public PixKeyService()
        {
            _pixKeyRepository = DependencyService.Get<IPixKeyRepository>();
            _externalActionService = DependencyService.Get<IExternalActionService>();
            _eventService = DependencyService.Get<IEventService>();
        }

        public bool IsValid(PixKey pixKey)
        {
            return !string.IsNullOrWhiteSpace(pixKey?.Key);
        }

        public List<PixKey> GetAll()
        {
            return _pixKeyRepository.GetAll();
        }

        public PixKey GetFirst()
        {
            return _pixKeyRepository.GetFirst();
        }

        public bool Update(PixKey item)
        {
            return _pixKeyRepository.Update(item);
        }

        public bool Insert(PixKey item)
        {
            return _pixKeyRepository.Insert(item);
        }

        public bool Remove(PixKey item)
        {
            return _pixKeyRepository.Remove(item);
        }

        public void ShareAllKeys()
        {
            var pisKeyList = GetAll();

            if (!HasKeysValidated())
                return;

            var info = "";

            foreach (var item in pisKeyList)
            {
                info += item.Viewer.InstitutionAndKey + "\n";
            }

            if (string.IsNullOrWhiteSpace(info))
                return;

            try
            {
                var options = new List<ActionSheetOption>()
                {
                    new ActionSheetOption("Compartilhar em texto", async () =>
                    {
                        await _externalActionService.ShareText(info);
                    }),
                    new ActionSheetOption("Salvar em txt e compartilhar", async () =>
                    {
                        var path = _externalActionService.GenerateTxtFile(info, "ChavesPix");
                        await _externalActionService.ShareFile(path);
                    }),
                };

                DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
                {
                    Title = "Selecione uma opção:",
                    Options = options,
                    Cancel = new ActionSheetOption("Cancelar", () =>
                    {
                        return;
                    })
                });
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Compartilhou todas as chaves", PixQrCodeGeneratorOffline.Services.EventType.SHARE);
            }
        }

        public async Task<bool> RemoveAll()
        {
            var pisKeyList = GetAll();

            if (!HasKeysValidated())
                return false;

            var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir todas as " + pisKeyList.Count + " chaves?", "Confirmação", "Sim, tenho certeza", "Cancelar");

            if (!confirm)
                return false;

            try
            {
                var success = _pixKeyRepository.RemoveAll();

                if (success)
                    DialogService.Toast("Todas as chaves foram removidas com sucesso!");

                else
                    DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app");


                return success;
            }
            catch (Exception e)
            {
                e.SendToLog();
                return false;
            }
            finally
            {
                _eventService.SendEvent("Removeu todas as chaves", PixQrCodeGeneratorOffline.Services.EventType.CRUD);
            }
        }

        public async Task NavigateToEdit(DashboardViewModel dashboardVM, PixKey pixKey)
        {
            try
            {
                DialogService.ShowLoading("");

                await Task.Delay(500);

                await Shell.Current.Navigation.PushModalAsync(new AddPixKeyPage(dashboardVM, pixKey));
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Editou chave", EventType.CRUD);

                DialogService.HideLoading();
            }
        }

        public async Task NavigateToAdd(DashboardViewModel dashboardVM)
        {
            try
            {
                DialogService.ShowLoading("");

                await Task.Delay(500);

                await Shell.Current.Navigation.PushModalAsync(new AddPixKeyPage(dashboardVM));
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Navegou para adicionar nova chave", EventType.NAVIGATION);

                DialogService.HideLoading();
            }
        }

        public async Task NavigateToAction(DashboardViewModel dashboardVM, PixKey pixKey)
        {
            try
            {
                DialogService.ShowLoading("");

                await Task.Delay(500);

                await Shell.Current.Navigation.PushModalAsync(new PixKeyActionPage(dashboardVM, pixKey));
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Navegou para ação", EventType.NAVIGATION);

                DialogService.HideLoading();
            }
        }

        private bool HasKeysValidated()
        {
            var pisKeyList = GetAll();

            if (!(pisKeyList.Count > 0))
            {
                DialogService.Toast("Nenhuma chave encontrada");
                return false;
            }

            return true;
        }
    }
}
