using PixQrCodeGeneratorOffline.Base.ViewModels;
using System;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class WebViewViewModel : ViewModelBase
    {
        public WebViewViewModel(Uri uri)
        {
            CurrentUri = uri;
        }


        private Uri _currentUri;
        public Uri CurrentUri
        {
            set => SetProperty(ref _currentUri, value);
            get => _currentUri;
        }
    }
}
