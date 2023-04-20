using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class PicPay : InstitutionBase, IInstitutionBank
    {
        public string Name => "PicPay";

        public FinancialInstitutionType Type => FinancialInstitutionType.PicPay;

        public new string Icon => FontBancos.PicPay;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "picpay",
            Primary = Color.FromHex("#23c45c"),
            PrimaryDark = Color.FromHex("#00922f"),
            PrimaryLight = Color.FromHex("#67f88b"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
