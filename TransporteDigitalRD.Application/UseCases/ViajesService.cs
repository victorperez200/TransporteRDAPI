using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Data;
using TransporteDigitalRD.Data.Entities;

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
            var viajeList = _db.Viaje.ToList();
            var viajeResponse = new List<ViajeResponse>();

            foreach (var item in viajeList)
            {
                var response = new ViajeResponse
                {
                    nombre_ruta = item.nombre_ruta, // Asignar el nombre de la ruta
                    ViajeId = item.viaje_id,
                    TipoId = item.tipo_id,
                    OrigenLat = item.origen_lat,
                    OrigenLong = item.origen_lng,
                    DestLat = item.origen_lat,
                    DestLong = item.origen_lng,
                    FechaInicio = item.fecha_inicio,
                    FechaFin = item.fecha_fin,
                    Costo = item.costo,
                    UbicActual = item.Ubicacion_actual,
                    Destino = item.Destino,
                    Estado=item.estado
                };

                viajeResponse.Add(response);
            }

            return viajeResponse;
        }

        public async Task<List<ViajeResponse>> GetHistorialViajesPorUsuario(int usuarioId)
        {
            // Filtrar los viajes por usuario y por los estados 'Terminado' o 'Cancelado'
            var viajeList = _db.Viaje_Usuario
                .Where(v => v.UsuarioID == usuarioId && (v.estado == "Terminado" || v.estado == "Cancelado"))
                .ToList();  // Obtiene los viajes filtrados

            var viajeResponse = new List<ViajeResponse>();

            // Mapear los datos de la tabla 'Viajes' a 'ViajeResponse'
            foreach (var item in viajeList)
            {
                var response = new ViajeResponse
                {
                    nombre_ruta = item.nombre_ruta, // Asignar el nombre de la ruta
                    Origen = item.origen,
                    ViajeId = item.viaje_id,
                    TipoId = item.tipo_id,
                    OrigenLat = item.origen_lat,
                    OrigenLong = item.origen_lng,
                    DestLat = item.origen_lat,
                    DestLong = item.origen_lng,
                    FechaInicio = item.fecha_inicio,
                    FechaFin = item.fecha_fin,
                    Costo = item.costo,
                    UbicActual = item.Ubicacion_actual,
                    Destino = item.Destino,
                    Estado = item.estado  // Incluir el estado del viaje
                };

                viajeResponse.Add(response);
            }

            return viajeResponse;
        }


        public async Task<Viaje_Actualrequest> GetViajeActual(int id)
        {
            var viaje = _db.Viaje_Usuario.FirstOrDefault(x => x.UsuarioID == id && (x.estado == "Disponible"));

            if (viaje == null) return null;
            var response = new Viaje_Actualrequest
            {
                nombre_actual = viaje.nombre_ruta, // Asegúrate de que 'nombre_actual' esté definido en tu modelo
                UserId = viaje.UsuarioID,
                ViajeId = viaje.viaje_id,
                Estado = viaje.estado,
                UbicacionActual = viaje.Ubicacion_actual,
                Destino = viaje.Destino,
                Origen = viaje.origen, // Asegúrate de que 'Origen' esté definido en tu modelo
                FechaInicio = (DateTime)viaje.fecha_inicio,
                FechaFin = (DateTime)viaje.fecha_fin,
                Costo = (decimal)viaje.costo,
                OrigenLat = (double)viaje.origen_lat,
                OrigenLong = (double)viaje.origen_lng,
                DestLat = (double)viaje.destino_lat,
                DestLong = (double)viaje.destino_lng,
                TipoId = viaje.tipo_id // Asegúrate de que 'TipoId' sea del tipo correcto
            };
            return response;
        }
        private static readonly HashSet<string> ESTADOS_PERMITIDOS =
    new(StringComparer.OrdinalIgnoreCase) { "Pendiente", "Disponible", "Finalizado", "Cancelado" };

        private static string NormalizarEstado(string? estado)
        {
            var e = (estado ?? "").Trim();
            if (string.IsNullOrEmpty(e)) return "Pendiente";
            return ESTADOS_PERMITIDOS.Contains(e) ? ESTADOS_PERMITIDOS.First(x => x.Equals(e, StringComparison.OrdinalIgnoreCase)) : "Pendiente";
        }

        public async Task <bool> PutViajeActual(int userId, Viaje_ActualUpdate dto)
        {
            var v = _db.Viaje_Usuario
        .Where(x => x.UsuarioID == userId && (x.estado == "Disponible" || x.estado == "Pendiente"))
        .OrderByDescending(x => x.fecha_inicio)
        .FirstOrDefault();

            if (v is null) return true;

            if (!string.IsNullOrWhiteSpace(dto.UbicacionActual)) v.Ubicacion_actual = dto.UbicacionActual;
            if (!string.IsNullOrWhiteSpace(dto.Destino)) v.Destino = dto.Destino;
            if (!string.IsNullOrWhiteSpace(dto.Origen)) v.origen = dto.Origen;

            if (!string.IsNullOrWhiteSpace(dto.Estado))
            {
                var estado = NormalizarEstado(dto.Estado);
                v.estado = dto.Estado;

                // Si finaliza, sella fecha_fin
                if (dto.Estado.Equals("Terminado", StringComparison.OrdinalIgnoreCase))
                    v.fecha_fin = DateTime.UtcNow;
            }

            _db.SubmitChanges();
            return false;
        }
        public async Task<bool> PostViajeActual(Viaje_Actualrequest dto)
        {
            var viaje = _db.Viaje.FirstOrDefault(x => x.viaje_id == dto.ViajeId);
            if (viaje == null) return true;

            var nuevoViaje = new Viaje_Usuario
            {
                nombre_ruta = viaje.nombre_ruta, // Asignar el nombre de la ruta
                viaje_id =viaje.viaje_id,
                UsuarioID =dto.UserId , // Asignar el ID del usuario
                origen = viaje.origen,
                tipo_id = viaje.tipo_id,
                origen_lat = viaje.origen_lat,
                origen_lng = viaje.origen_lng,
                destino_lat = viaje.destino_lat,
                destino_lng = viaje.destino_lng,
                fecha_inicio = (DateTime)viaje.fecha_inicio,
                fecha_fin = viaje.fecha_fin,
                costo = viaje.costo,
                Ubicacion_actual = viaje.Ubicacion_actual,
                Destino = viaje.Destino,
                estado = viaje.estado // Asignar el estado del viaje
            };

            _db.Viaje_Usuario.InsertOnSubmit(nuevoViaje);

            try
            {
                _db.SubmitChanges(); // Guardar cambios en la base de datos
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        public async Task<bool> CreateViaje(CreateViajeDto dto)
        {
            if (dto == null) return false;

            var nuevoViaje = new Viaje
            {
                nombre_ruta = dto.NombreRuta, // Asignar el nombre de la ruta
                origen =dto.Origen,
                tipo_id = dto.TipoId,
                origen_lat = dto.OrigenLat,
                origen_lng = dto.OrigenLong,
                destino_lat = dto.DestLat,
                destino_lng = dto.DestLong,
                fecha_inicio = dto.FechaInicio,
                fecha_fin = dto.FechaFin,
                costo = dto.Costo,
                Ubicacion_actual = dto.UbicActual,
                Destino = dto.Destino,
                estado=dto.Estado // Asignar el estado del viaje
            };

            _db.Viaje.InsertOnSubmit(nuevoViaje);

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
            if (id < 0 || !_db.Viaje.Any(x => x.viaje_id == id)) return false;

            var viaje = _db.Viaje.FirstOrDefault(x => x.viaje_id == id);

            _db.Viaje.DeleteOnSubmit(viaje);

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

        ////
        // Agregar este método a tu ViajesService existente

        public async Task<ViajeResponse> UpdateViaje(int id, UpdateViajeDto dto)
        {
            if (id <= 0 || dto == null)
                throw new ArgumentException("ID inválido o DTO nulo");

            // Buscar el viaje existente
            var viajeExistente = _db.Viaje.FirstOrDefault(x => x.viaje_id == id);

            if (viajeExistente == null)
            {
                throw new KeyNotFoundException($"No se encontró el viaje con ID: {id}");
            }

            // Actualizar las propiedades usando los nombres de columna correctos
            viajeExistente.nombre_ruta = dto.Nombre_ruta; // Asignar el nombre de la ruta
            viajeExistente.tipo_id = dto.TipoId;
            viajeExistente.origen_lat = dto.OrigenLat;
            viajeExistente.origen_lng = dto.OrigenLong;
            viajeExistente.destino_lat = dto.DestLat;
            viajeExistente.destino_lng = dto.DestLong;
            viajeExistente.fecha_inicio = dto.FechaInicio;
            viajeExistente.fecha_fin = dto.FechaFin;
            viajeExistente.costo = dto.Costo;
            viajeExistente.Ubicacion_actual = dto.UbicActual;
            viajeExistente.Destino = dto.Destino;
            viajeExistente.estado = dto.Estado; // Actualizar el estado del viaje

            try
            {
                _db.SubmitChanges(); // Guardar cambios en la base de datos

                // Retornar el viaje actualizado como ViajeResponse
                return new ViajeResponse
                {
                    ViajeId = viajeExistente.viaje_id,
                    TipoId = viajeExistente.tipo_id,
                    OrigenLat = viajeExistente.origen_lat,
                    OrigenLong = viajeExistente.origen_lng,
                    DestLat = viajeExistente.destino_lat,
                    DestLong = viajeExistente.destino_lng,
                    FechaInicio = viajeExistente.fecha_inicio,
                    FechaFin = viajeExistente.fecha_fin,
                    Costo = viajeExistente.costo,
                    UbicActual = viajeExistente.Ubicacion_actual,
                    Destino = viajeExistente.Destino,
                    Estado = viajeExistente.estado // Incluir el estado actualizado
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el viaje: {ex.Message}");
            }
        }


    }
}
