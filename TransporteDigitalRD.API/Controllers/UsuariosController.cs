using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Application.UseCases;

namespace TransporteDigitalRD.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class UsuariosController : ControllerBase
  {
    private readonly UsuariosService _usuariosService;
    public UsuariosController(UsuariosService usariosService)
    {
      _usuariosService = usariosService;
    }

    [HttpGet] // TODO: maybe añadir un parametro {token} y ver si es admin, pero por ahora no tiene verificaciones
    public async Task<IActionResult> GetUsuariosAsync()
    {
      var response = _usuariosService.GetUsuarios();
      return Ok(response);
    }

    [HttpPut("me")]
    public async Task<IActionResult> PutMeAsync([FromBody] PutMeRequest request)
    {
      var response = _usuariosService.PutMe(request);
      return Ok(response);

    }

    [HttpPost("Foto")]
    public async Task<IActionResult> FotoAsync(FotoRequest request)
    {
      var response = _usuariosService.PostFoto(request);
      return Ok(response);
    }
  }
}
