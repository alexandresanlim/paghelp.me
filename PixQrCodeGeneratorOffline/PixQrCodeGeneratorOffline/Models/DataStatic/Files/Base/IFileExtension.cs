namespace PixQrCodeGeneratorOffline.Models.DataStatic.Files.Base
{
    public interface IFileExtension
    {
        public string Display { get; }

        public string ContentType { get; }

        public string SetOnFileName { get; }
    }
}
