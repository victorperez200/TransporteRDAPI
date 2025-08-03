using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransporteDigitalRD.Data;

namespace TransporteDigitalRD.Application.DTOs
{
  public class PutMeRequest
  {
    public string Token { get; set; }
    public Usuario usuario { get; set; }
  }
}
