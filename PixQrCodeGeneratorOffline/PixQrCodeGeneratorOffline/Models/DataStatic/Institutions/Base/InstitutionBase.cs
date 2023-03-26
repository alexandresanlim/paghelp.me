using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base
{
    public class InstitutionBase
    {
        protected readonly IMaterialColorService _materialColorService;

        public string Icon => FontBancos.Pix;

        public InstitutionBase()
        {
            _materialColorService = DependencyService.Get<IMaterialColorService>();
        }
    }
}
