namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces
{
    public interface IInstitutionCrypto
    {
        FinancialInstitutionCryptoType Type { get; }

        string Name { get; }

        string Code { get; }

        string LinkToWallet { get; }

        MaterialColor MaterialColor { get; }

        public string Icon { get; }
    }
}
