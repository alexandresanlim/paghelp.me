using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Style;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class FinancialInstitution
    {
        private readonly IMaterialColorService _materialColorService;

        private readonly IFinancialInstitutionService _financialInstitutionService;

        public FinancialInstitution()
        {
            _materialColorService = DependencyService.Get<IMaterialColorService>();
            _financialInstitutionService = DependencyService.Get<IFinancialInstitutionService>();
        }

        public string Name { get; set; }

        //public FinancialInstitutionType Type { get; set; }

        //public string LogoUri { get; set; }

        //public bool AvailablePremium { get; set; }

        [LiteDB.BsonIgnore]
        public Institution Institution => _financialInstitutionService.GetInstitutionInstance(this) ?? new Institution();

        //[LiteDB.BsonIgnore]
        //public IInstitution Institution => _financialInstitutionService?.GetInstitution(this) ?? new DataStatic.Institutions.NaoInformado();

        //[LiteDB.BsonIgnore]
        //public MaterialColor Color => _materialColorService.GetColorByFinancialInstitution(this);
    }

    public enum FinancialInstitutionType
    {
        None,
        BancodoBrasil,
        BancoInter,
        BancoBMG,
        BancoBTGPactual,
        BancoOriginal,
        BancoPan,
        Bradesco,
        BS2,
        C6Bank,
        Caixa,
        Digio,
        Gerencianet,
        Itau,
        MercadoPago,
        Neon,
        Nubank,
        Next,
        PagBank,
        PicPay,
        Safra,
        Santander,
        Sicredi,
        Superdigital,
        Iti,
    }
}
