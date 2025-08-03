using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransporteDigitalRD.Data;

namespace TransporteDigitalRD.Application.DTOs
{
  public class UsuariosResponse
  {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }
        public string Foto { get; set; }
        public string Roles { get; set; }
        public DateTime? FechaRegistro { get; set; }
  }
}
