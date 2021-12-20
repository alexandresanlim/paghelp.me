using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixKeyService : ServiceBase, IPixKeyService
    {
        private readonly IPixKeyRepository _pixKeyRepository;

        private readonly IExternalActionService _externalActionService;

        public PixKeyService()
        {
            _pixKeyRepository = DependencyService.Get<IPixKeyRepository>();
            _externalActionService = DependencyService.Get<IExternalActionService>();
        }

        public bool IsValid(PixKey pixKey)
        {
            return !string.IsNullOrWhiteSpace(pixKey?.Key);
        }

        public List<PixKey> GetAll(bool isContact = false)
        {
            return _pixKeyRepository.GetAll(x => x.IsContact == isContact);
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

        public async Task NavigateToShareAllKeys(ObservableCollection<PixKey> pixkeyList)
        {
            try
            {
                DialogService.ShowLoading("");

                await Task.Delay(500);

                await Shell.Current.Navigation.PushAsync(new ShareKeyPage(pixkeyList));
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

        public async Task ShareAllKeys(string info)
        {
            try
            {
                DialogService.ShowLoading("");

                await Task.Delay(500);


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

            var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir todas as " + pisKeyList.Count + " chaves?", "Confirmação", "Sim, tenho certeza", "Cancelar");

            if (!confirm)
                return false;

            try
            {
                var success = _pixKeyRepository.RemoveAll(x => x.IsContact == isContact);

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
                _eventService.SendEvent("Removeu todas as chaves", PixQrCodeGeneratorOffline.Services.EventType.DELETE);
            }
        }

        public async Task NavigateToEdit(PixKey pixKey, bool isContact = false)
        {
            if (!pixKey.Validation.IsValid)
                return;

            try
            {
                DialogService.ShowLoading("");

                await Task.Delay(500);

                await Shell.Current.Navigation.PushAsync(new AddPixKeyPage(pixKey, isContact));
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Editou chave", EventType.UPDATE);

                DialogService.HideLoading();
            }
        }

        public async Task NavigateToAdd(bool isContact = false)
        {
            try
            {
                DialogService.ShowLoading("");

                await Task.Delay(500);

                await Shell.Current.Navigation.PushAsync(new AddPixKeyPage(null, isContact));
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

        //public async Task NavigateToAction(PixKey pixKey)
        //{
        //    if (!pixKey.Validation.IsValid)
        //        return;

        //    try
        //    {
        //        DialogService.ShowLoading("");

        //        await Task.Delay(500);

        //        await Shell.Current.Navigation.PushModalAsync(new PixKeyActionPage(pixKey));
        //    }
        //    catch (System.Exception e)
        //    {
        //        e.SendToLog();
        //    }
        //    finally
        //    {
        //        _eventService.SendEvent("Navegou para ação", EventType.NAVIGATION);

        //        DialogService.HideLoading();
        //    }
        //}

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
