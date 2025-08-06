using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransporteDigitalRD.Application.DTOs
{
    public class CreateReportarProblemaDto
    {
        public int TipoProblemaId { get; set; }
        public int TipoTransporteId { get; set; }
        public double Origen_Lat { get; set; }
        public double Origen_Lng { get; set; }
        public string Desc_Problema { get; set; }
    }
}
