using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IPixPayloadService
    {
        PixPayload Create(PixKey pixKey);

        PixPayload Create(PixKey pixKey, PixCob pixCob);

        bool IsValid(PixPayload pixPayload);

        bool Save(PixPayload pixPaylod);

        List<PixPayload> GetAll(Expression<Func<PixPayload, bool>> predicate = null);

        Task<bool> RemoveAll(Expression<Func<PixPayload, bool>> predicate = null);
    }
}
