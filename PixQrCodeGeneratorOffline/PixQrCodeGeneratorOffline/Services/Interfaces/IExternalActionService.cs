using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IExternalActionService
    {
        Task ShareText(string text);

        Task CopyText(string text, string textSuccess = "Copiado com sucesso!");
    }
}
