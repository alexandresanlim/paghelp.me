using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Validation;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class PixCob : NotifyObjectBase
    {
        private readonly IPixCobViewerService _pixCobViewerService;

        private readonly IPixCobValidationService _pixCobValidationService;

        public PixCob()
        {
            _pixCobViewerService = DependencyService.Get<IPixCobViewerService>();
            _pixCobValidationService = DependencyService.Get<IPixCobValidationService>();
        }

        private string _value;
        public string Value
        {
            set { SetProperty(ref _value, value); }
            get { return _value; }
        }

        private string _description;
        public string Description
        {
            set { SetProperty(ref _description, value); }
            get { return _description; }
        }

        public PixCobViewer Viewer => _pixCobViewerService?.Create(this) ?? new PixCobViewer();

        public PixCobValidation Validation => _pixCobValidationService?.Create(this) ?? new PixCobValidation();
    }
}
