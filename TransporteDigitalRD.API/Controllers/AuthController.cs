using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Application.UseCases;

namespace TransporteDigitalRD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);
            if (response == null)
                return Conflict(new { message = "El correo ya está registrado" });
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }

        [HttpGet("me/{token}")]
        public async Task<IActionResult> GetMeAsync(string token)
        {
          var response = _authService.GetMe(new MeRequest{ Token = token});
          return Ok(response);
        }

        [HttpGet("roles/{token}")]
        public async Task<IActionResult> RolesAsync(string token)
        {
          var response = await _authService.GetRoles(new RoleRequest { Token = token});
          return Ok(response);

        }
    }
  }
