using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base
{
    public class InstitutionBase
    {
        protected readonly IMaterialColorService _materialColorService;

        public InstitutionBase()
        {
            _materialColorService = DependencyService.Get<IMaterialColorService>();
        }
    }
}
