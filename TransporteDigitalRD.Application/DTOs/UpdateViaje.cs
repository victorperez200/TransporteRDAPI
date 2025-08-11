using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransporteDigitalRD.Application.DTOs
{
    public class UpdateViajeDto
    {
        public string Nombre_ruta { get; set; } 
        public int UsuarioId { get; set; }
        public int TipoId { get; set; }
        public double OrigenLat { get; set; }
        public double OrigenLong { get; set; }
        public double DestLat { get; set; }
        public double DestLong { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Costo { get; set; }
        public string UbicActual { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public string Estado { get; set; }// Estado por defecto al actualizar un viaje
    }
}
