using AsyncAwaitBestPractices.MVVM;
using System;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Models.Commands.Interfaces
{
    public interface ICustomAsyncCommand
    {
        AsyncCommand Create(Func<Task> execute);
    }
}
