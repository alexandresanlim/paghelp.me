using PixQrCodeGeneratorOffline.Models.DataStatic.Files.Base;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Files
{
    public class TxtFile : IFileExtension
    {
        public string Display => "TXT";

        public string ContentType => "text/plain";

        public string SetOnFileName => ".txt";
    }
}
