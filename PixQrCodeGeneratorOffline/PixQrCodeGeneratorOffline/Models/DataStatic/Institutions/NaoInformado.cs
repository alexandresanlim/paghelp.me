using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class NaoInformado : InstitutionBase, IInstitutionBank
    {
        public string Name => "Não Informado";

        public FinancialInstitutionType Type => FinancialInstitutionType.None;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "naoinformado",
            Primary = Color.FromHex("#34495e"),
            PrimaryDark = Color.FromHex("#092234"),
            PrimaryLight = Color.FromHex("#60748b"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
