using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Data;

namespace TransporteDigitalRD.Application.UseCases
{
    public class ViajesService
    {
        private readonly TransRDDataContext _db;
        public ViajesService(TransRDDataContext db) 
        {
            _db = db;
        }

        public async Task<List<ViajeResponse>> GetViajes()
        {
            var viajeList = _db.Viajes.ToList();
            var viajeResponse = new List<ViajeResponse>();

            foreach (var item in viajeList)
            {
                var response = new ViajeResponse
                {
                    ViajeId = item.viaje_id,
                    UsuarioId = item.usuario_id,
                    TipoId = item.tipo_id,
                    OrigenLat = item.origen_lat,
                    OrigenLong = item.origen_lng,
                    DestLat = item.origen_lat,
                    DestLong = item.origen_lng,
                    FechaInicio = item.fecha_inicio,
                    FechaFin = item.fecha_fin,
                    Costo = item.costo,
                    UbicActual = item.Ubicacion_actual,
                    Destino = item.Destino
                };

                viajeResponse.Add(response);
            }

            return viajeResponse;
        }


        public async Task<bool> CreateViaje(CreateViajeDto dto)
        {
            if (dto == null) return false;

            var nuevoViaje = new Viaje
            {
                usuario_id = dto.UsuarioId,
                tipo_id = dto.TipoId,
                origen_lat = dto.OrigenLat,
                origen_lng = dto.OrigenLong,
                destino_lat = dto.DestLat,
                destino_lng = dto.DestLong,
                fecha_inicio = dto.FechaInicio,
                fecha_fin = dto.FechaFin,
                costo = dto.Costo,
                Ubicacion_actual = dto.UbicActual,
                Destino = dto.Destino
            };

            _db.Viajes.InsertOnSubmit(nuevoViaje);

            try
            {
                _db.SubmitChanges(); // Guardar cambios en la base de datos
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteViaje(int id)
        {
            if (id < 0 || !_db.Viajes.Any(x => x.viaje_id == id)) return false;

            var viaje = _db.Viajes.FirstOrDefault(x => x.viaje_id == id);

            _db.Viajes.DeleteOnSubmit(viaje);

            try
            {
                _db.SubmitChanges(); // Guardar cambios en la base de datos
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
