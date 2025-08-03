using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Application.UseCases;

namespace TransporteDigitalRD.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class Postulaciones : ControllerBase
  {
    // Una postulacion es un boleto basicamente
    private readonly PostulacionesService postulacionesService;
    public Postulaciones(PostulacionesService service)
    {
      postulacionesService = service;
    }
    [HttpPost]
    public async Task<IActionResult> PostPostulacion([FromBody] PostulacionRequest request)
    {
      var response = await postulacionesService.PostPostulacion(request);
      return Ok(response);
    }
    [HttpGet("{token}")]
    public async Task<IActionResult> GetPostulaciones(string token)
    {
      var response = await postulacionesService.GetPostulaciones(new PostulacionesRequest { token = token });
      return Ok(response);
    }
  }
}
