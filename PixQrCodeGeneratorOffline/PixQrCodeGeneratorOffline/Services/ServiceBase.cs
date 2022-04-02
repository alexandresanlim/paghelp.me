using Acr.UserDialogs;
using CsvHelper;
using CsvHelper.Configuration;
using PixQrCodeGeneratorOffline.Models.DataStatic.Files;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Services
{
    public class ServiceBase
    {
        protected IUserDialogs DialogService => UserDialogs.Instance;

        protected readonly IEventService _eventService;

        protected readonly IExternalActionService _externalActionService;

        private bool _isLoading;

        protected readonly TxtFile _txtFile = new TxtFile();

        protected readonly CsvFile _csvFile = new CsvFile();

        protected static MemoryStream _mem = new MemoryStream();

        protected readonly CsvWriter _csvWriter = new CsvWriter(new StreamWriter(_mem, Encoding.UTF8), new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            Encoding = Encoding.UTF8,
        });

        public ServiceBase()
        {
            _eventService = DependencyService.Get<IEventService>();
            _externalActionService = DependencyService.Get<IExternalActionService>();
        }

        public void SetIsLoading(bool isLoading = true, string title = "")
        {
            _isLoading = isLoading;

            if (_isLoading)
                DialogService.ShowLoading(title);

            else
                DialogService.HideLoading();
        }
    }
}
