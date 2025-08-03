using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransporteDigitalRD.Application.DTOs
{
    public class TipoTransporteResponse
    {
        public int TipoId { get; set; }
        public string Nombre { get; set; }
        public decimal TarifaBase {  get; set; }
    }
}
