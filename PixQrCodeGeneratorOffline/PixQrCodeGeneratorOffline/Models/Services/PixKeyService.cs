using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Helpers;
using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixKeyService : ServiceBase, IPixKeyService
    {
        private readonly IPixKeyRepository _pixKeyRepository;

        private readonly IPixKeyViewerService _pixKeyViewerService;

        private readonly IPixPayloadService _pixPayloadService;

        private readonly IPixKeyCommand _pixKeyCommand;

        public PixKeyService()
        {
            _pixKeyRepository = DependencyService.Get<IPixKeyRepository>();
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
                            var path = _externalActionService.BuildPathFile(info, "ChavesPix", _txtFile);
                            await _externalActionService.ShareFile(path, _txtFile);
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

        public async Task ExportToFile(IList<PixKey> pixkeyList)
        {
            _csvWriter.WriteField("Chave");
            _csvWriter.WriteField("Instituicao");
            _csvWriter.WriteField("CodigoCopiaECola");
            _csvWriter.NextRecord();

            foreach (var key in pixkeyList)
            {
                _csvWriter.WriteField(key.Key);
                _csvWriter.WriteField(string.IsNullOrWhiteSpace(key?.FinancialInstitution?.Name) ? "Não informado" : key.FinancialInstitution.Name);
                _csvWriter.WriteField(string.IsNullOrWhiteSpace(key?.Payload?.QrCode) ? "Não encontrado" : key.Payload.QrCode);
                _csvWriter.NextRecord();
            }

            await _csvWriter.FlushAsync();

            var result = Encoding.UTF8.GetString(_mem.ToArray());

            BuildPathAndShareKeys(result);
        }

        public async Task ExportToFileContact(IList<PixKey> contactPixkeyList)
        {
            _csvWriter.WriteField("Nome");
            _csvWriter.WriteField("Chave");
            _csvWriter.WriteField("Cidade");
            _csvWriter.NextRecord();

            foreach (var key in contactPixkeyList)
            {
                _csvWriter.WriteField(string.IsNullOrWhiteSpace(key?.Name) ? "Não informado" : key.Name);
                _csvWriter.WriteField(key?.Key);
                _csvWriter.WriteField(string.IsNullOrWhiteSpace(key?.City) ? "Não informado" : key.City);
                _csvWriter.NextRecord();
            }

            await _csvWriter.FlushAsync();

            var result = Encoding.UTF8.GetString(_mem.ToArray());

            BuildPathAndShareKeysContact(result);
        }

        const EventType EXPORT_EVENT_TYPE = EventType.GENERATEFILE;
        const string EXPORT_EVENT_CLASS = nameof(PixKeyService);

        private void BuildPathAndShareKeys(string result)
        {
            var fileName = $"minhas-chaves-pix-{DateTime.Now.ToString("dd-MM-yy-HH-mm")}";

            const string EXPORT_EVENT_TITLE = "Exportou minhas chaves em ";

            var options = new List<ActionSheetOption>()
            {
                new ActionSheetOption(_csvFile.Display, async () =>
                {
                    try
                    {
                        var path = _externalActionService.BuildPathFile(result, fileName, _csvFile);
                        await _externalActionService.ShareFile(path, _csvFile);
                        _eventService.SendEvent(EXPORT_EVENT_TITLE + _csvFile.Display, EXPORT_EVENT_TYPE, EXPORT_EVENT_CLASS);
                    }
                    catch (Exception e)
                    {
                        e.SendToLog();
                    }
                }),
                new ActionSheetOption(_txtFile.Display, async () =>
                {
                    try
                    {
                        var path = _externalActionService.BuildPathFile(result, fileName, _txtFile);
                        await _externalActionService.ShareFile(path, _txtFile);
                        _eventService.SendEvent(EXPORT_EVENT_TITLE + _txtFile.Display, EXPORT_EVENT_TYPE, EXPORT_EVENT_CLASS);
                    }
                    catch (Exception e)
                    {
                        e.SendToLog();
                    }
                }),
            };

            DialogService.ActionSheet(new ActionSheetConfig
            {
                Title = Constants.EXPORT_FILE_READY_SELECT_FORMAT,
                Options = options,
                Cancel = new ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        }

        private void BuildPathAndShareKeysContact(string result)
        {
            const string EXPORT_EVENT_TITLE = "Exportou chaves de contatos em ";

            var fileName = $"contatos-chaves-pix-{DateTime.Now.ToString("dd-MM-yy-HH-mm")}";

            var options = new List<ActionSheetOption>()
            {
                new ActionSheetOption(_csvFile.Display, async () =>
                {
                    try
                    {
                        var path = _externalActionService.BuildPathFile(result, fileName, _csvFile);
                        await _externalActionService.ShareFile(path, _csvFile);
                        _eventService.SendEvent( EXPORT_EVENT_TITLE+ _csvFile.Display, EXPORT_EVENT_TYPE, EXPORT_EVENT_CLASS);
                    }
                    catch (Exception e)
                    {
                        e.SendToLog();
                    }
                }),
                new ActionSheetOption(_txtFile.Display, async () =>
                {
                    try
                    {
                        var path = _externalActionService.BuildPathFile(result, fileName, _txtFile);
                        await _externalActionService.ShareFile(path, _txtFile);
                        _eventService.SendEvent(EXPORT_EVENT_TITLE + _txtFile.Display, EXPORT_EVENT_TYPE, EXPORT_EVENT_CLASS);
                    }
                    catch (Exception e)
                    {
                        e.SendToLog();
                    }
                }),
            };

            DialogService.ActionSheet(new ActionSheetConfig
            {
                Title = Constants.EXPORT_FILE_READY_SELECT_FORMAT,
                Options = options,
                Cancel = new ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
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
                _eventService.SendEvent("Removeu todas as chaves", EventType.DELETE);
            }
        }

        public async Task NavigateToAdd(bool isContact = false)
        {
            SetIsLoading(true);

            await Shell.Current.Navigation.PushPopupAsync(new AddPixKeyPage(null, isContact));

            SetIsLoading(false);
        }

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
