using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Extention
{
    public static class UriExtension
    {
        public static async Task<(bool IsSuccess, string Result)> GetHtml(this Uri uri)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var html = await webClient.DownloadStringTaskAsync(uri).ConfigureAwait(false);
                    return(true, html);
                }
            }
            catch (WebException wex)
            {
                return(false, wex.Message);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
