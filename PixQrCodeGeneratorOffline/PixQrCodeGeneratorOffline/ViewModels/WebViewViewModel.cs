using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class WebViewViewModel : BaseViewModel
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
