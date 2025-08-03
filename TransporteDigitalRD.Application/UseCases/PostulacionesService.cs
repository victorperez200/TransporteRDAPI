using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Data;

namespace TransporteDigitalRD.Application.UseCases
{
  public class PostulacionesService
  {
    private readonly TransRDDataContext _db;
    public PostulacionesService (TransRDDataContext db)
    {
      _db = db;
    }

    public async Task<PostulacionesResponse> GetPostulaciones(PostulacionesRequest request)
    {
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadJwtToken(request.token);

      if (token == null) return null;

      var _usuario = _db.Usuarios.SingleOrDefault(u => u.usuario_id.ToString() == token.Claims.FirstOrDefault(c => c.Type == "User.Id").Value);

      var boletos = _db.Boletos.Where(b => b.usuario_id == _usuario.usuario_id).ToList();
      return new PostulacionesResponse
      {
        Postulaciones = boletos
      };
    }

    public async Task<PostulacionResponse> PostPostulacion(PostulacionRequest request)
    {
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadJwtToken(request.token);

      if (token == null) return null;

      var _usuario = _db.Usuarios.SingleOrDefault(u => u.usuario_id.ToString() == token.Claims.FirstOrDefault(c => c.Type == "User.Id").Value);
      var _viaje = _db.Viajes.SingleOrDefault(v => v.viaje_id.ToString() == request.viaje_id);

      if (_usuario == null || _viaje == null) return null;

      var _boleto = new Boleto
      {
        usuario_id = _usuario.usuario_id,
        viaje_id = _viaje.viaje_id,
        fecha_compra = DateTime.Now,
        monto = _viaje.costo ?? 0,
        estado = "Disponible"
      };

      _db.Boletos.InsertOnSubmit(_boleto);

      _db.SubmitChanges();

      return new PostulacionResponse
      {
        boleto = _boleto
      };

    }
  }
}
