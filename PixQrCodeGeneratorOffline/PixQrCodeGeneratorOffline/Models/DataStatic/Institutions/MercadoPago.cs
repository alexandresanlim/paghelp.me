using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class MercadoPago : InstitutionBase, IInstitutionBank
    {
        public string Name => "Mercado Pago";

        public FinancialInstitutionType Type => FinancialInstitutionType.MercadoPago;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "mercadopago",
            Primary = Color.FromHex("#05abf3"),
            PrimaryDark = Color.FromHex("#007cc0"),
            PrimaryLight = Color.FromHex("#67ddff"),
            TextOnPrimary = Color.FromHex("#000000")
        };
    }
}
