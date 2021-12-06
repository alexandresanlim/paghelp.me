using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IExternalActionService
    {
        Task ShareText(string text);

        Task ShareOnWhats(string text, string phoneNumber = null);

        Task CopyText(string text, string textSuccess = "Copiado com sucesso!");

        string GenerateTxtFile(string contents, string fileName);

        Task ShareFile(string path);
    }
}
