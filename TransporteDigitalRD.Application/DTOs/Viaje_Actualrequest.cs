using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransporteDigitalRD.Application.DTOs
{
    public class Viaje_Actualrequest
    {
        public string nombre_actual { get; set; }
        public int UserId { get; set; }
        public int ViajeId { get; set; }
        public string Estado { get; set; }
        public string UbicacionActual { get; set; }
        public string Destino { get; set; }
        public string Origen { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Costo { get; set; }
        public double OrigenLat { get; set; }
        public double OrigenLong { get; set; }
        public double DestLat { get; set; }
        public double DestLong { get; set; }
        public int TipoId { get; set; }

       
    }
}
