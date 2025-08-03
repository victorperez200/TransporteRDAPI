using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Application.Interfaces;
using TransporteDigitalRD.Data;
using TransporteDigitalRD.Data.Entities;

namespace TransporteDigitalRD.Application.UseCases
{
        public class AuthService
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenGenerator _tokenGenerator;
            private readonly TransRDDataContext _db;

            public AuthService(IUserRepository userRepository, ITokenGenerator tokenGenerator, TransRDDataContext db)
            {
                _userRepository = userRepository;
                _tokenGenerator = tokenGenerator;
                _db = db;
            }

            public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
            {
               /* var existing = await _userRepository.GetByEmailAsync(request.Email);
                if (existing != null)
                    throw new Exception("User already exists");*/

                var user = new Usuario
                {
                    nombre = request.Username,
                    email = request.Email,
                    telefono = request.NumberPhone,
                    fecha_registro = DateTime.UtcNow,
                    estado = request.Status,
                    contraseña = HashPassword(request.Password)
                };


                _db.Usuarios.InsertOnSubmit(user);
                _db.SubmitChanges();
               // await _userRepository.AddAsync(user);

                return new AuthResponse
                {
                    Token = _tokenGenerator.GenerateToken(user),
                    Username = user.nombre
                };
            }

            public async Task<AuthResponse> LoginAsync(LoginRequest request)
              {
              var user = _db.Usuarios.SingleOrDefault(u => u.email == request.Email);

              if (user == null || user.contraseña != HashPassword(request.Password))
                            throw new Exception("Invalid credentials");

                return new AuthResponse
                {
                    Token = _tokenGenerator.GenerateToken(user),
                    Username = user.nombre
                };
            }

            public async Task<MeResponse> GetMe(MeRequest request)
            {
              var handler = new JwtSecurityTokenHandler();
              var token = handler.ReadJwtToken(request.Token);

              if (token == null) return null;

              var _usuario = _db.Usuarios.SingleOrDefault(u => u.usuario_id.ToString() == token.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

              return new MeResponse
              {
                usuario = _usuario
              };
            }
            public async Task<RoleResponse> GetRoles(RoleRequest request)
            {
              var handler = new JwtSecurityTokenHandler();
              var token = handler.ReadJwtToken(request.Token);

              if (token == null) return null;

              var _usuario = _db.Usuarios.SingleOrDefault(u => u.usuario_id.ToString() == token.Claims.FirstOrDefault(c => c.Type == "User.Id").Value);

              return new RoleResponse
              {
                roles = _usuario.roles
              };
            }

            private string HashPassword(string password)
            {
                using var sha = SHA256.Create();
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
          }
}


