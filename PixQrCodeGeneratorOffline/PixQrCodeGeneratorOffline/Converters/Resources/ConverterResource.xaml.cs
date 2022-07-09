using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Converters.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConverterResource : ResourceDictionary
    {
        public ConverterResource()
        {
            InitializeComponent();
        }
    }
}