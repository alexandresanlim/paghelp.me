using PixQrCodeGeneratorOffline.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IFeedService
    {
        Task<List<Feed>> Get(string feedUrl);
    }
}
