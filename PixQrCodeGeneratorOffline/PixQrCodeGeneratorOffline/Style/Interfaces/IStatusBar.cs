namespace PixQrCodeGeneratorOffline.Style.Interfaces
{
    public interface IStatusBar
    {
        void SetColor();

        void SetStatusBarColor(System.Drawing.Color statusBar, System.Drawing.Color? navigationBar = null);
    }
}
