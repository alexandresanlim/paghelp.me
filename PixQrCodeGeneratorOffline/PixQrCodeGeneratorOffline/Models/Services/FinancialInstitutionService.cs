using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class FinancialInstitutionService : IFinancialInstitutionService
    {
        public FinancialInstitution Create(FinancialInstitutionType financialInstitutionType, bool availablePremium = false)
        {
            return new FinancialInstitution
            {
                Name = GetNameByType(financialInstitutionType),
                Type = financialInstitutionType,
                //AvailablePremium = availablePremium
            };
        }

        public List<FinancialInstitution> GetList()
        {
            return new List<FinancialInstitution>
            {
                Create(GetType(new NaoInformado())),
                Create(GetType(new BancoBMG())),
                Create(GetType(new BancoBTGPactual())),
                Create(GetType(new BancoDoBrasil())),
                Create(GetType(new BancoInter())),
                Create(GetType(new BancoOriginal())),
                Create(GetType(new BancoPan())),
                Create(GetType(new Bradesco())),
                Create(GetType(new BS2())),
                Create(GetType(new C6Bank())),
                Create(GetType(new Caixa())),
                Create(GetType(new Digio())),
                Create(GetType(new Gerencianet())),
                Create(GetType(new Itau())),
                Create(GetType(new Iti())),
                Create(GetType(new MercadoPago())),
                Create(GetType(new Neon())),
                Create(GetType(new Next())),
                Create(GetType(new Nubank())),
                Create(GetType(new PagBank())),
                Create(GetType(new PicPay())),
                Create(GetType(new Safra())),
                Create(GetType(new Santander())),
                Create(GetType(new Sicredi())),
                Create(GetType(new SuperDigital()))
            };
        }

        private string GetNameByType(FinancialInstitutionType financialInstitutionType)
        {
            switch (financialInstitutionType)
            {
                case FinancialInstitutionType.BancodoBrasil:
                    return GetName(new BancoDoBrasil());

                case FinancialInstitutionType.BancoInter:
                    return GetName(new BancoInter());

                case FinancialInstitutionType.BancoBMG:
                    return GetName(new BancoBMG());

                case FinancialInstitutionType.BancoBTGPactual:
                    return GetName(new BancoBTGPactual());

                case FinancialInstitutionType.BancoOriginal:
                    return GetName(new BancoOriginal());

                case FinancialInstitutionType.BancoPan:
                    return GetName(new BancoPan());

                case FinancialInstitutionType.Bradesco:
                    return GetName(new Bradesco());

                case FinancialInstitutionType.BS2:
                    return GetName(new BS2());

                case FinancialInstitutionType.C6Bank:
                    return GetName(new C6Bank());

                case FinancialInstitutionType.Caixa:
                    return GetName(new Caixa());

                case FinancialInstitutionType.Digio:
                    return GetName(new Digio());

                case FinancialInstitutionType.Gerencianet:
                    return GetName(new Gerencianet());

                case FinancialInstitutionType.Itau:
                    return GetName(new Itau());

                case FinancialInstitutionType.MercadoPago:
                    return GetName(new MercadoPago());

                case FinancialInstitutionType.Neon:
                    return GetName(new Neon());

                case FinancialInstitutionType.Nubank:
                    return GetName(new Nubank());

                case FinancialInstitutionType.Next:
                    return GetName(new Next());

                case FinancialInstitutionType.PagBank:
                    return GetName(new PagBank());

                case FinancialInstitutionType.PicPay:
                    return GetName(new PicPay());

                case FinancialInstitutionType.Safra:
                    return GetName(new Safra());

                case FinancialInstitutionType.Santander:
                    return GetName(new Santander());

                case FinancialInstitutionType.Sicredi:
                    return GetName(new Sicredi());

                case FinancialInstitutionType.Superdigital:
                    return GetName(new SuperDigital());

                case FinancialInstitutionType.Iti:
                    return GetName(new Iti());

                case FinancialInstitutionType.None:
                default:
                    return "Não Informado";
            }
        }

        //public IInstitution GetInstitution(FinancialInstitution financialInstitution)
        //{
        //    return string.IsNullOrWhiteSpace(financialInstitution?.Name)
        //        ? new NaoInformado()
        //        : GetInstitutionList()?.FirstOrDefault(x => x.Name.ToLower().Equals(financialInstitution?.Name?.ToLower())) ?? new NaoInformado();
        //}

        public Institution GetInstitutionInstance(FinancialInstitution financialInstitution)
        {
            var i = GetInstitutionList()?.FirstOrDefault(x => x.Type == financialInstitution.Type || x.Name.ToLower().Equals(financialInstitution?.Name?.ToLower())) ?? new NaoInformado();

            return ToInstitution(i);
        }

        private Institution ToInstitution(IInstitutionBank institution)
        {
            return new Institution
            {
                Name = GetName(institution),
                Type = GetType(institution),
                MaterialColor = GetMaterialColor(institution)
            };
        }

        public string GetName(IInstitutionBank institution)
        {
            return institution.Name;
        }

        public MaterialColor GetMaterialColor(IInstitutionBank institution)
        {
            return institution.MaterialColor;
        }

        public FinancialInstitutionType GetType(IInstitutionBank institution)
        {
            return institution.Type;
        }

        public List<IInstitutionBank> GetInstitutionList()
        {
            return new List<IInstitutionBank>
            {
                new BancoBMG(),
                new BancoBTGPactual(),
                new BancoDoBrasil(),
                new BancoInter(),
                new BancoOriginal(),
                new BancoPan(),
                new Bradesco(),
                new BS2(),
                new C6Bank(),
                new Caixa(),
                new Digio(),
                new Gerencianet(),
                new Itau(),
                new Iti(),
                new MercadoPago(),
                new Neon(),
                new Next(),
                new Nubank(),
                new PagBank(),
                new PicPay(),
                new Safra(),
                new Santander(),
                new Sicredi(),
                new SuperDigital()
            };
        }
    }
}
