using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using PixQrCodeGeneratorOffline.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixKeyService : ServiceBase, IPixKeyService
    {
        private readonly IPixKeyRepository _pixKeyRepository;

        private readonly IExternalActionService _externalActionService;

        private readonly IPixKeyViewerService _pixKeyViewerService;

        private readonly IPixPayloadService _pixPayloadService;

        private readonly IPixKeyCommand _pixKeyCommand;

        public PixKeyService()
        {
            _pixKeyRepository = DependencyService.Get<IPixKeyRepository>();
            _externalActionService = DependencyService.Get<IExternalActionService>();
            _pixKeyViewerService = DependencyService.Get<IPixKeyViewerService>();
            _pixPayloadService = DependencyService.Get<IPixPayloadService>();
            _pixKeyCommand = DependencyService.Get<IPixKeyCommand>();
        }

        public List<PixKey> GetAll(bool isContact = false)
        {
            var list = _pixKeyRepository.GetAll(x => x.IsContact == isContact)
                .OrderBy(x => x?.FinancialInstitution?.Name);

            foreach (var pixKey in list)
            {
                pixKey.Viewer = _pixKeyViewerService?.Create(pixKey) ?? new PixKeyViewer();
                pixKey.Payload = _pixPayloadService?.Create(pixKey) ?? new PixPayload();
                pixKey.Command = _pixKeyCommand?.Create(pixKey) ?? new PixKeyCommand();
            }

            return list.ToList();
        }

        public List<PixKey> GetAll(Expression<Func<PixKey, bool>> predicate)
        {
            return _pixKeyRepository.GetAll(predicate);
        }

        public PixKey GetById(int id)
        {
            return _pixKeyRepository.FindById(id);
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

        public async Task NavigateToShareAllKeys(ObservableCollection<PixKey> pixkeyList) =>
            await Shell.Current.Navigation.PushAsync(new ShareKeyPage(pixkeyList));

        public void ShareAllKeys(string info)
        {
            try
            {
                DialogService.ShowLoading("");

                var options = new List<ActionSheetOption>()
                    {
                        new ActionSheetOption("Compartilhar", async () =>
                        {
                            await _externalActionService.ShareText(info);
                        }),
                        new ActionSheetOption("Salvar em .txt e compartilhar", async () =>
                        {
                            var path = _externalActionService.GenerateTxtFile(info, "ChavesPix");
                            await _externalActionService.ShareFile(path);
                        }),
                    };

                DialogService.ActionSheet(new ActionSheetConfig
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
                _eventService.SendEvent("Navegou para página de compartilhar todas as chaves", EventType.NAVIGATION);

                DialogService.HideLoading();
            }
        }

        public async Task<bool> RemoveAll(bool isContact = false)
        {
            var pisKeyList = _pixKeyRepository.GetAll(x => x.IsContact == isContact);

            if (!HasKeysValidated(pisKeyList))
                return false;

            var title = pisKeyList.Count.Equals(1) ?
                "Tem certeza que deseja excluir a última chave?" :
                "Tem certeza que deseja excluir todas as " + pisKeyList.Count + " chaves?";

            var confirm = await DialogService.ConfirmAsync(title, "Confirmação", "Sim, tenho certeza", "Cancelar");

            if (!confirm)
                return false;

            try
            {
                var success = _pixKeyRepository.RemoveAll(x => x.IsContact == isContact);

                if (success)
                    DialogService.Toast("Chaves removidas com sucesso!");

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
                _eventService.SendEvent("Removeu todas as chaves", PixQrCodeGeneratorOffline.Services.EventType.DELETE);
            }
        }

        public async Task NavigateToAdd(bool isContact = false) => await Shell.Current.Navigation.PushPopupAsync(new AddPixKeyPage(null, isContact));

        private bool HasKeysValidated(List<PixKey> pisKeyList)
        {
            if (!(pisKeyList.Count > 0))
            {
                DialogService.Toast("Nenhuma chave encontrada");
                return false;
            }

            return true;
        }
    }
}
