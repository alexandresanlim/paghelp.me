namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions
{
    public static class PixCobExtension
    {
        public static bool IsValid(this PixCob pixCob) => !string.IsNullOrEmpty(pixCob?.Value);
    }
}
