using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto.Services
{
    public class CryptoKeyViewerService : ICryptoKeyViewerService
    {
        public CryptoKeyViewer Create(CryptoKey pixKey)
        {
            return pixKey.Validation.IsValid ? new CryptoKeyViewer
            {
                KeyPresentation = GetKeyPresentation(pixKey),
                InstitutionPresentation = GetInstitutionPresentation(pixKey),
                InstitutionAndKey = GetInstitutionAndKey(pixKey)
            } : new CryptoKeyViewer();
        }

        private string GetKeyPresentation(CryptoKey pixKey) => pixKey?.Key;

        private string GetInstitutionPresentation(CryptoKey pixKey) => pixKey?.FinancialInstitution?.Name;

        private string GetInstitutionAndKey(CryptoKey pixKey) => "Instituição: " + (!string.IsNullOrEmpty(pixKey?.FinancialInstitution?.Name) ? pixKey?.FinancialInstitution?.Name : "Não informado") + " | Chave: " + pixKey?.Key;
    }
}
