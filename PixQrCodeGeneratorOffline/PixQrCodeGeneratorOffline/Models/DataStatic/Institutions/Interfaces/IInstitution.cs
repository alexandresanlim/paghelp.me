using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces
{
    public interface IInstitution
    {
        string Name { get; }

        FinancialInstitutionType Type { get; }

        MaterialColor Color { get; }
    }
}
