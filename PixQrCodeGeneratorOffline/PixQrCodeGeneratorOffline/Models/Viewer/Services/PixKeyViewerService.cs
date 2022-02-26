using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;

namespace PixQrCodeGeneratorOffline.Models.Viewer.Services
{
    public class PixKeyViewerService : IPixKeyViewerService
    {
        public PixKeyViewer Create(PixKey pixKey)
        {
            return pixKey.Validation.IsValid ? new PixKeyViewer
            {
                NameAndCity = GetNameAndCity(pixKey),
                NamePresentation = GetNamePresentation(pixKey),
                KeyPresentation = GetKeyPresentation(pixKey),
                InstitutionPresentation = GetInstitutionPresentation(pixKey),
                InstitutionAndKey = GetInstitutionAndKey(pixKey),
                BankAndKey = GetBankAndKey(pixKey),
                Initial = GetInitial(pixKey),
            } : new PixKeyViewer();
        }

        private string GetNameAndCity(PixKey pixKey) => (!string.IsNullOrEmpty(pixKey?.Name) && !string.IsNullOrEmpty(pixKey?.City)) ? pixKey?.Name + ", " + pixKey?.City : "";

        private string GetNamePresentation(PixKey pixKey) => pixKey?.Name;

        private string GetKeyPresentation(PixKey pixKey) => pixKey?.Key;

        private string GetInstitutionPresentation(PixKey pixKey) => pixKey?.FinancialInstitution?.Name;

        private string GetInstitutionAndKey(PixKey pixKey) => "Instituição: " + (!string.IsNullOrEmpty(pixKey?.FinancialInstitution?.Name) ? pixKey?.FinancialInstitution?.Name : "Não informado") + " | Chave: " + pixKey?.Key;

        private string GetBankAndKey(PixKey pixKey) => (!string.IsNullOrEmpty(pixKey?.FinancialInstitution?.Name) ? pixKey?.FinancialInstitution?.Name : "Não informado") + " | " + pixKey?.Key;

        private string GetInitial(PixKey pixKey)
        {
            try
            {
                var name = pixKey?.Name;

                if (!pixKey.IsContact || string.IsNullOrWhiteSpace(name))
                    return "";

                string first = name.Substring(0, 1);
                string last;

                if (!(name.Length > 1))
                    last = first;

                else if (!name.Contains(" "))
                    last = name.Substring(1, 1);

                else
                {
                    var split = name.Split(' ');
                    last = split[1].Substring(0, 1);
                }

                return first + last;
            }
            catch (System.Exception)
            {
                return "**";
            }
        }
    }
}
