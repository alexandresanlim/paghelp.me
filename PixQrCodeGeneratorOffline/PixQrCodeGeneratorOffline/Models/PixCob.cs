using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class PixCob : NotifyObjectBase
    {
        private readonly IPixCobViewerService _pixCobViewerService;

        public PixCob()
        {
            _pixCobViewerService = DependencyService.Get<IPixCobViewerService>();
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
    }
}
