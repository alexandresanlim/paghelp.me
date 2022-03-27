using PixQrCodeGeneratorOffline.Models.DataStatic.Files.Base;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Files
{
    public class CsvFile : IFileExtension
    {
        public string Display => "CSV";

        public string ContentType => "text/csv";

        public string SetOnFileName => ".csv";
    }
}
