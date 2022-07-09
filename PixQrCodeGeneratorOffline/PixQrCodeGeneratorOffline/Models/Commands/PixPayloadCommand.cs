using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.IO;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands
{
    public class PixPayloadCommand : CommandBase, IPixPayloadCommand
    {
        public IAsyncCommand NavigateToPaymentPageCommand { get; private set; }

        public IAsyncCommand DownloadQrCodeCommand { get; private set; }

        public PixPayloadCommand Create(PixPayload pixPayload)
        {
            return new PixPayloadCommand
            {
                NavigateToPaymentPageCommand = GetNavigateToPaymentPageCommand(pixPayload),
                DownloadQrCodeCommand = GetDownloadQrCodeCommand(pixPayload)
            };
        }

        private IAsyncCommand GetNavigateToPaymentPageCommand(PixPayload pixPayload) =>
            _customAsyncCommand.Create(async () => await Shell.Current.Navigation.PushPopupAsync(new PaymentPage(pixPayload)));

        private IAsyncCommand GetDownloadQrCodeCommand(PixPayload pixPayload) =>
            _customAsyncCommand.Create(async () =>
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                { 
                    DialogService.Toast("Ops! No momento está função está disponível somente conectado a internet", TimeSpan.FromSeconds(4));
                    return;
                }

                try
                {
                    DialogService.ShowLoading("");

                    var url = "https://chart.googleapis.com/chart?chs=400x400&cht=qr&chl=" + pixPayload.QrCode;

                    using (var webClient = new HttpClient())
                    {
                        var imageBytes = webClient.GetByteArrayAsync(url).Result;

                        var stream1 = new MemoryStream(imageBytes);
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        string filename = Path.Combine(path, pixPayload?.PixKey?.Key + "QRCode.png");

                        File.WriteAllBytes(filename, imageBytes);

                        await Share.RequestAsync(new ShareFileRequest
                        {
                            Title = "Para onde deseja enviar o QR Code?",
                            File = new ShareFile(filename)
                        });
                    }
                }
                catch (Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    DialogService.HideLoading();
                }
            });
    }
}
