namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions
{
    public static class PixCobExtension
    {
        public static bool IsValid(this PixCob pixCob) => !string.IsNullOrEmpty(pixCob?.Value);

        public static bool HasValue(this PixCob pixCob) => !string.IsNullOrEmpty(pixCob?.Value) && !pixCob.Value.Equals("0.00") && !pixCob.Value.Equals("0,00");
    }
}
