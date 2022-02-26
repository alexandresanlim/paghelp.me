using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands.Base
{
    public class CustomAsyncCommand : ICustomAsyncCommand
    {
        protected readonly IEventService _eventService;

        public CustomAsyncCommand()
        {
            _eventService = DependencyService.Get<IEventService>();
        }

        public AsyncCommand Create(Func<Task> execute)
        {
            return new AsyncCommand(execute, null, (ex) => ex.SendToLog());
        }
    }
}
