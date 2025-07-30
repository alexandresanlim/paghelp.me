namespace PixQrCodeGeneratorOffline.Extention
{
    public static class IconExtention
    {
        public static string GetIconFontFamily(FontAwesomeType type = FontAwesomeType.solid)
        {
            switch (type)
            {
                default:
                case FontAwesomeType.solid:
                    return "FontAwesomeSolid";

                case FontAwesomeType.brand:
                    return "FontAwesomeBrands";

                case FontAwesomeType.FonteBancosBrasileiros:
                    return "FonteBancosBrasileiros";
            }
        }

        public enum FontAwesomeType
        {
            solid,
            brand,
            FonteBancosBrasileiros
        }
    }
}
