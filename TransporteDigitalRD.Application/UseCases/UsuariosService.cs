using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Application.Interfaces;
using TransporteDigitalRD.Data;
using TransporteDigitalRD.Data.Entities;

namespace TransporteDigitalRD.Application.UseCases
{
  public class UsuariosService
  {
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly TransRDDataContext _db;

    public UsuariosService(IUserRepository userRepository, ITokenGenerator tokenGenerator, TransRDDataContext db)
    {
      _userRepository = userRepository;
      _tokenGenerator = tokenGenerator;
      _db = db;
    }

    public async Task<UsuariosResponse> GetUsuarios()
    {
      var usuarios = _db.Usuarios.ToList();
      return new UsuariosResponse
      {
        usuarios = usuarios,
      };
    }

    public async Task<FotoResponse> PostFoto(FotoRequest request)
    {
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadJwtToken(request.Token);

      if (token == null) return null;

      var _usuario = _db.Usuarios.SingleOrDefault(u => u.usuario_id.ToString() == token.Claims.FirstOrDefault(c => c.Type == "User.Id").Value);

      _usuario.foto = request.Foto;
      _db.SubmitChanges();

      return new FotoResponse
      {
        user = _usuario
      };
    }

    public async Task<PutMeResponse?> PutMe(PutMeRequest request)
    {
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadJwtToken(request.Token);
      var updatedUser = request.usuario;

      if (token == null) return null;

      var _usuario = _db.Usuarios.SingleOrDefault(u => u.usuario_id.ToString() == token.Claims.FirstOrDefault(c => c.Type == "User.Id").Value);

      if (_usuario == null) return null;

      var old_user = _usuario;
      _usuario.nombre = updatedUser.nombre;
      _usuario.email = updatedUser.email;
      _usuario.roles = updatedUser.roles;
      _usuario.CuentaSaldos = updatedUser.CuentaSaldos;
      _usuario.Boletos = updatedUser.Boletos;
      _usuario.estado = updatedUser.estado;
      _usuario.contraseña = updatedUser.contraseña;
      _usuario.telefono = updatedUser.telefono;
      _usuario.Viajes = updatedUser.Viajes;

      _db.SubmitChanges();

      return new PutMeResponse
      {
        old_user = old_user,
        usuario = _usuario
      };
    }
  }
}
