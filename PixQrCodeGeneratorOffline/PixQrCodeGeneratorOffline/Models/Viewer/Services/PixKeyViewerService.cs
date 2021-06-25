using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
                BankAndKey = GetBankAndKey(pixKey)
            } : new PixKeyViewer();
        }

        private string GetNameAndCity(PixKey pixKey) => (!string.IsNullOrEmpty(pixKey?.Name) && !string.IsNullOrEmpty(pixKey?.City)) ? pixKey?.Name + ", " + pixKey?.City : "";

        private string GetNamePresentation(PixKey pixKey) => !string.IsNullOrEmpty(pixKey?.Name) ? pixKey?.Name : "";

        private string GetKeyPresentation(PixKey pixKey) => !string.IsNullOrEmpty(pixKey?.Key) ? "Chave: " + pixKey?.Key : "";

        private string GetInstitutionPresentation(PixKey pixKey) => !string.IsNullOrEmpty(pixKey?.FinancialInstitution?.Name) ? pixKey?.FinancialInstitution?.Name : "";

        private string GetInstitutionAndKey(PixKey pixKey) => "Instituição: " + (!string.IsNullOrEmpty(pixKey?.FinancialInstitution?.Name) ? pixKey?.FinancialInstitution?.Name : "Não informado") + " | Chave: " + pixKey?.Key;

        private string GetBankAndKey(PixKey pixKey) => (!string.IsNullOrEmpty(pixKey?.FinancialInstitution?.Name) ? pixKey?.FinancialInstitution?.Name : "Não informado") + " | Chave: " + pixKey?.Key;
    }
}
