using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomProgressLayoutAnimated : ProgressBar
    {
        public CustomProgressLayoutAnimated()
        {
            ProgressColor = Color.White;
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
