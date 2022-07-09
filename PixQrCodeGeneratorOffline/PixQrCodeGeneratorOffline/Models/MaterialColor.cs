using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class MaterialColor
    {
        private readonly IMaterialColorService _materialColorService;

        public MaterialColor()
        {
            _materialColorService = DependencyService.Get<IMaterialColorService>();

            //TextPrimary = _materialColorService.GetByCurrentDeviceTheme().TextPrimary;
            //TextSecondary = _materialColorService.GetByCurrentDeviceTheme().TextSecondary;
            //BackgroundPage = _materialColorService.GetByCurrentDeviceTheme().BackgroundPage;
            //ForegroundPage = _materialColorService.GetByCurrentDeviceTheme().ForegroundPage;
            Secondary = Color.FromHex("#50000000");
            TextOnSecondary = Color.FromHex("#ffffff");
            IsDarkOrLightTheme = false;
        }

        public string Name { get; set; }

        public Color Primary { get; set; }

        public Color PrimaryLight { get; set; }

        public Color PrimaryDark { get; set; }

        public Color PrimaryTransparence => Primary.SetTransparence(0.1);

        public Color Secondary { get; set; }

        public Color SecondaryLight { get; set; }

        public Color SecondaryDark { get; set; }

        public Color TextOnPrimary { get; set; }

        public Color TextOnSecondary { get; set; }

        public Color BackgroundPage { get; set; }

        public Color ForegroundPage { get; set; }

        public Color White => Color.White;

        public Color Black => Color.Black;

        public Color TextPrimary { get; set; }

        public Color TextSecondary { get; set; }

        public bool IsDarkOrLightTheme { get; set; }
    }
}
