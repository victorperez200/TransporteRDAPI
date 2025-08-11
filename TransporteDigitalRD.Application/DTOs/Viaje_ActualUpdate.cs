using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransporteDigitalRD.Application.DTOs
{
    public class Viaje_ActualUpdate
    {
        public string? UbicacionActual { get; set; }
        public string? Destino { get; set; }
        public string? Origen { get; set; }
        public string? Estado { get; set; } // Pendiente/EnCurso/Finalizado/Cancelado
    }
}
