using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class FinancialInstitution : FinancialInstitutionBase
    {
        private readonly IFinancialInstitutionService _financialInstitutionService;

        public FinancialInstitution()
        {
            _financialInstitutionService = DependencyService.Get<IFinancialInstitutionService>();
        }

        public FinancialInstitutionType Type { get; set; }

        [LiteDB.BsonIgnore]
        public InstitutionBank Institution => _financialInstitutionService.GetInstitutionInstance(this) ?? new InstitutionBank();
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
