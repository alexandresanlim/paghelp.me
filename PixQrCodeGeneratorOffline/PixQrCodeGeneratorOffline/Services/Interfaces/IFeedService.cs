using PixQrCodeGeneratorOffline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IFeedService
    {
        Task<IList<Feed>> Get(string feedUrl);
    }
}
