using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransporteDigitalRD.Data;
using TransporteDigitalRD.Data.Entities;

namespace TransporteDigitalRD.Application.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(Usuario user);

    }
}
