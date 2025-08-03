using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Data;

namespace TransporteDigitalRD.Application.UseCases
{
  public class OportunidadesService
  {
    TransRDDataContext _db;
    public OportunidadesService(TransRDDataContext db) {
      _db = db;
    }
    public async Task<OportunidadesResponse> GetOportunidades()
    {
      var _viajes = _db.Viajes.ToList();
      return new OportunidadesResponse
      {
        Viajes = _viajes
      };
    }
    public async Task<OportunidadesSingleResponse> GetOportunidades(OportunidadesRequest request)
    {
      var viaje = _db.Viajes.SingleOrDefault(v => v.viaje_id.ToString() == request.id);
      return new OportunidadesSingleResponse
      {
        viaje = viaje
      };
    }
  }
}
