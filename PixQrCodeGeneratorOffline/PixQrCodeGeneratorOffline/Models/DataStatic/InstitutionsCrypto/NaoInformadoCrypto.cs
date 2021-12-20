using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto
{
    public class NaoInformadoCrypto : InstitutionBase, IInstitutionCrypto
    {
        public string Name => "Não Informado";

        public FinancialInstitutionCryptoType Type => FinancialInstitutionCryptoType.None;

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
