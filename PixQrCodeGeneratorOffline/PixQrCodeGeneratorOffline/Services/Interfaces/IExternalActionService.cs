using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IExternalActionService
    {
        Task ShareText(string text);

        Task ShareOnWhats(string text, string phoneNumber = null);

        Task CopyText(string text, string textSuccess = "Copiado com sucesso!", Color? backgroundToast = null, Color? foregroundToast = null);

        string GenerateTxtFile(string contents, string fileName);

        Task ShareFile(string path);
    }
}
