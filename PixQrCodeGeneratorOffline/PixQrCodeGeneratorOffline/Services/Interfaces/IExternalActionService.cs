using PixQrCodeGeneratorOffline.Models.DataStatic.Files.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IExternalActionService
    {
        Task ShareText(string text);

        Task ShareOnWhats(string text, string phoneNumber = null);

        Task CopyText(string text, string textSuccess = "Copiado com sucesso!", Color? backgroundToast = null, Color? foregroundToast = null);

        string BuildPathFile(string contents, string fileName, IFileExtension extension);

        Task ShareFile(string path, IFileExtension extension);
    }
}
