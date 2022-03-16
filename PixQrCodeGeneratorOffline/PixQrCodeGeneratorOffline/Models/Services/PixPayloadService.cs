using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Extention;
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
using System.Linq.Expressions;
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

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                Cobranca cobranca = new Cobranca(_chave: pixKey.Key);

                pixPaylod.Payload = cobranca?.ToPayload("PAGHELPME" + Guid.NewGuid().ToString("N").Substring(0, 10), new Merchant(pixKey?.Name, pixKey?.City));

                pixPaylod.QrCode = pixPaylod.Payload?.GenerateStringToQrCode();
            });

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
    }
}
