namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces
{
    public interface IInstitutionBank
    {
        FinancialInstitutionType Type { get; }

        string Name { get; }

        MaterialColor MaterialColor { get; }

        public string Icon { get; }
    }
}
