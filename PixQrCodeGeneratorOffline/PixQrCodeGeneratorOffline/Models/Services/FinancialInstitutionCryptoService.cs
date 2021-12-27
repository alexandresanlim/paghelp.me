using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class FinancialInstitutionCryptoService : IFinancialInstitutionCryptoService
    {
        public FinancialInstitutionCrypto Create(FinancialInstitutionCryptoType financialInstitutionType)
        {
            return new FinancialInstitutionCrypto
            {
                Name = GetNameByType(financialInstitutionType),
                Type = financialInstitutionType,
            };
        }

        public InstitutionCrypto GetInstitutionInstance(FinancialInstitutionCrypto financialInstitution)
        {
            var i = GetInstitutionList()?.FirstOrDefault(x => x.Type == financialInstitution.Type || x.Name.ToLower().Equals(financialInstitution?.Name?.ToLower())) ?? new NaoInformadoCrypto();

            return ToInstitution(i);
        }

        public List<FinancialInstitutionCrypto> GetList()
        {
            return new List<FinancialInstitutionCrypto>
            {
                Create(GetType(new Bitcoin())),
                Create(GetType(new BinanceCoin())),
                Create(GetType(new Ethereum())),
                Create(GetType(new Litecoin())),
            };
        }

        public string GetName(IInstitutionCrypto institution)
        {
            return institution.Name;
        }

        public MaterialColor GetMaterialColor(IInstitutionCrypto institution)
        {
            return institution.MaterialColor;
        }

        public FinancialInstitutionCryptoType GetType(IInstitutionCrypto institution)
        {
            return institution.Type;
        }

        public string GetLinkToWallet(IInstitutionCrypto institution)
        {
            return institution.LinkToWallet;
        }

        private string GetNameByType(FinancialInstitutionCryptoType financialInstitutionType)
        {
            switch (financialInstitutionType)
            {
                case FinancialInstitutionCryptoType.Bitcoin:
                    return GetName(new Bitcoin());

                case FinancialInstitutionCryptoType.Binancecoin:
                    return GetName(new BinanceCoin());

                case FinancialInstitutionCryptoType.Ethereum:
                    return GetName(new Ethereum());

                case FinancialInstitutionCryptoType.Litecoin:
                    return GetName(new Litecoin());

                case FinancialInstitutionCryptoType.None:
                default:
                    return "Não Informado";
            }
        }

        public List<IInstitutionCrypto> GetInstitutionList()
        {
            return new List<IInstitutionCrypto>
            {
                new Bitcoin(),
                new BinanceCoin(),
                new Ethereum(),
                new Litecoin()
            };
        }

        private InstitutionCrypto ToInstitution(IInstitutionCrypto institution)
        {
            return new InstitutionCrypto
            {
                Name = GetName(institution),
                Type = GetType(institution),
                LinkToWallet = GetLinkToWallet(institution),
                MaterialColor = GetMaterialColor(institution)
            };
        }
    }
}
