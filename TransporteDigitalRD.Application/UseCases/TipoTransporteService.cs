using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Data;

namespace TransporteDigitalRD.Application.UseCases
{
    public class TipoTransporteService
    {
        private readonly TransRDDataContext _db;

        public TipoTransporteService(TransRDDataContext db)
        {
            _db = db;
        }

        public async Task<List<TipoTransporteResponse>> GetTipoTransporte()
        {
            var transporteList = _db.TipoTransportes.ToList();
            var newListTransporte = new List<TipoTransporteResponse>();

            foreach (var item in transporteList)
            {
                var response = new TipoTransporteResponse
                {
                    TipoId = item.tipo_id,
                    Nombre = item.nombre,
                    TarifaBase = item.tarifa_base,
                };

                newListTransporte.Add(response);
            }

            return newListTransporte;
        }
    }
}
