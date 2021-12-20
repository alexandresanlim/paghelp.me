namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces
{
    public interface IInstitutionCrypto
    {
        FinancialInstitutionCryptoType Type { get; }

        string Name { get; }

        MaterialColor MaterialColor { get; }
    }
}
