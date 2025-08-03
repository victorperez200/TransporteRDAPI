using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransporteDigitalRD.Data;

namespace TransporteDigitalRD.Application.DTOs
{
  public class PostulacionRequest
  {
    public string token {  get; set; }
    public string viaje_id { get; set; }
  }
}
