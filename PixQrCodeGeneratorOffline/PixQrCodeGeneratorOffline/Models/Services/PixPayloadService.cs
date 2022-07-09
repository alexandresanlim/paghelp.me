using Acr.UserDialogs;
using CsvHelper;
using CsvHelper.Configuration;
using pix_payload_generator.net.Models.Attributes;
using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Helpers;
using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixPayloadService : ServiceBase, IPixPayloadService
    {
        private readonly IPixPayloadRepository _pixPayloadRepository;

        private readonly IPixPayloadCommand _pixPayloadCommand;

        private readonly IPixKeyViewerService _pixKeyViewerService;

        public PixPayloadService()
        {
            _pixPayloadRepository = DependencyService.Get<IPixPayloadRepository>();
            _pixPayloadCommand = DependencyService.Get<IPixPayloadCommand>();
            _pixKeyViewerService = DependencyService.Get<IPixKeyViewerService>();
        }

        public PixPayload Create(PixKey pixKey)
        {
            if (!pixKey.IsValid())
                return new PixPayload();

            var pixPaylod = new PixPayload
            {
                PixKey = pixKey
            };


            Cobranca cobranca = new Cobranca(_chave: pixKey?.Key);

            var cobrancaIsValid = cobranca.IsValid();

            if (!cobrancaIsValid)
            {
                DialogService.Toast("Cobrança inválida.");
                _eventService.SendEvent("Cobrança inválida", EventType.FLOW, nameof(PixPayloadService), cobranca.IsValidString());
                return null;
            }

            pixPaylod.Payload = cobranca?.ToPayload("PAGHELPME" + Guid.NewGuid().ToString("N").Substring(0, 10), new Merchant(pixKey?.Name, pixKey?.City));
            pixPaylod.QrCode = pixPaylod.Payload?.GenerateStringToQrCode();
            pixPaylod.Commands = _pixPayloadCommand.Create(pixPaylod);

            return pixPaylod;
        }

        public PixPayload Create(PixKey pixKey, PixCob pixCob)
        {
            if (!pixKey.IsValid() || !pixCob.IsValid())
                return new PixPayload();

            var pixPaylod = new PixPayload
            {
                PixKey = pixKey,
                PixCob = pixCob,
            };

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                Cobranca cobranca = new Cobranca(_chave: pixKey.Key)
                {
                    SolicitacaoPagador = pixCob?.Description,
                    Valor = new Valor
                    {
                        Original = pixCob.Viewer.ValueFormatter
                    }
                };

                pixPaylod.Payload = cobranca?.ToPayload("PAGHELPME" + Guid.NewGuid().ToString("N").Substring(0, 10), new Merchant(pixKey?.Name, pixKey?.City));

                pixPaylod.QrCode = pixPaylod.Payload?.GenerateStringToQrCode();
            });

            pixPaylod.Commands = _pixPayloadCommand.Create(pixPaylod);

            return pixPaylod;
        }

        public bool Save(PixPayload pixPaylod)
        {
            var success = _pixPayloadRepository.Insert(pixPaylod);

            pixPaylod.Commands = _pixPayloadCommand.Create(pixPaylod);

            DialogService.Toast("Cobrança salva com sucesso!");

            return success;
        }

        public List<PixPayload> GetAll(Expression<Func<PixPayload, bool>> predicate = null)
        {
            var list = _pixPayloadRepository.GetAll(predicate);

            foreach (var item in list)
            {
                item.Commands = _pixPayloadCommand?.Create(item) ?? new PixPayloadCommand();
                item.PixKey.Viewer = _pixKeyViewerService?.Create(item?.PixKey) ?? new PixKeyViewer();
            }

            return list;
        }

        public async Task<bool> RemoveAll(Expression<Func<PixPayload, bool>> predicate = null)
        {
            var all = GetAll() ?? new List<PixPayload>();

            var count = all.Count;

            if (count == 0)
            {
                DialogService.Toast("Nenhuma cobrança salva foi encontrada.");
                return false;
            }

            var multiple = count > 1 ? "cobranças salvas" : "cobrança salva";

            var confirm = await DialogService.ConfirmAsync($"Tem certeza que deseja remover {count} {multiple} ?", "Confirmação", "Sim", "Cancelar");

            if (!confirm)
                return false;

            try
            {
                DialogService.ShowLoading("Aguarde...");

                var success = _pixPayloadRepository.RemoveAll(predicate);

                if (success)
                    DialogService.Toast("Todas as cobranças salvas foram removidas");

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
                _eventService.SendEvent("Removeu todas as cobranças salvar", EventType.DELETE);

                DialogService.HideLoading();
            }
        }

        public bool IsValid(PixPayload pixPayload)
        {
            return !string.IsNullOrEmpty(pixPayload?.QrCode);
        }

        public async Task ExportToFile(IList<PixPayload> saveBillingList)
        {
            _csvWriter.WriteField("Identificador");
            _csvWriter.WriteField("Instituicao");
            _csvWriter.WriteField("Chave");
            _csvWriter.WriteField("ValorCobranca");
            _csvWriter.WriteField("CodigoCopiaECola");
            _csvWriter.NextRecord();

            foreach (var billing in saveBillingList)
            {
                _csvWriter.WriteField(billing?.Identity);
                _csvWriter.WriteField(string.IsNullOrWhiteSpace(billing?.PixKey?.FinancialInstitution?.Name) ? "Não informado" : billing.PixKey.FinancialInstitution.Name);
                _csvWriter.WriteField(string.IsNullOrWhiteSpace(billing?.PixKey?.Key) ? "Não encontrado" : billing.PixKey.Key);
                _csvWriter.WriteField(string.IsNullOrWhiteSpace(billing?.PixCob?.Viewer?.ValuePresentation) ? "Não encontrado" : billing.PixCob.Viewer.ValuePresentation);
                _csvWriter.WriteField(string.IsNullOrWhiteSpace(billing?.QrCode) ? "Não encontrado" : billing.QrCode);
                _csvWriter.NextRecord();
            }

            await _csvWriter.FlushAsync();

            var result = Encoding.UTF8.GetString(_mem.ToArray());

            BuildPathAndShareBilling(result);
        }

        private void BuildPathAndShareBilling(string result)
        {
            var fileName = $"cobranças-salvas-pix-{DateTime.Now.ToString("dd-MM-yy-HH-mm")}";

            const string EXPORT_EVENT_TITLE = "Exportou cobranças salvas em ";
            const EventType EXPORT_EVENT_TYPE = EventType.GENERATEFILE;
            const string EXPORT_EVENT_CLASS = nameof(PixPayloadService);

            var options = new List<ActionSheetOption>()
            {
                new ActionSheetOption(_csvFile.Display, async () =>
                {
                    try
                    {
                        var path = _externalActionService.BuildPathFile(result, fileName, _csvFile);
                        await _externalActionService.ShareFile(path, _csvFile);
                        _eventService.SendEvent(EXPORT_EVENT_TITLE + _csvFile.Display, EXPORT_EVENT_TYPE , EXPORT_EVENT_CLASS);
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
                Cancel = new ActionSheetOption(Constants.CANCEL, () =>
                {
                    return;
                })
            });
        }
    }
}
