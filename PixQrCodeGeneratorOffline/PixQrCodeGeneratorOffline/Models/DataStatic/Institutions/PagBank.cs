using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class PagBank : InstitutionBase, IInstitutionBank
    {
        public string Name => "Pag Bank (PagSeguro)";

        public FinancialInstitutionType Type => FinancialInstitutionType.PagBank;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "pagbank",
            Primary = Color.FromHex("#5cbb4c"),
            PrimaryDark = Color.FromHex("#238a1c"),
            PrimaryLight = Color.FromHex("#8fee7b"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
