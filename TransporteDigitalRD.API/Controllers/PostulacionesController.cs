using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Application.UseCases;

namespace TransporteDigitalRD.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  //[Authorize]
  public class PostulacionesController : ControllerBase
  {
    // Una postulacion es un boleto basicamente
    private readonly PostulacionesService postulacionesService;
    public PostulacionesController(PostulacionesService service)
    {
      postulacionesService = service;
    }
    [HttpPost]
    public async Task<IActionResult> PostPostulacion([FromBody] PostulacionRequest request)
    {
      var response = await postulacionesService.PostPostulacion(request);
      if (response == null) return BadRequest("Invalid UserToke or TravelID (Token de sesion de usuario o id de viaje invalido)"); 
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
