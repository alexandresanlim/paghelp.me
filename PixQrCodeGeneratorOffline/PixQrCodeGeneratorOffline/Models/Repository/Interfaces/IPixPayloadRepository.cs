﻿using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PixQrCodeGeneratorOffline.Models.Repository.Interfaces
{
    public interface IPixPayloadRepository
    {
        List<PixPayload> GetAll(Expression<Func<PixPayload, bool>> predicate = null);

        PixPayload FindById(int id);

        bool Update(PixPayload item);

        bool Insert(PixPayload item);

        bool Remove(PixPayload item);

        bool RemoveAll(Expression<Func<PixPayload, bool>> predicate = null);
    }
}
