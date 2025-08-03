using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Linq.SqlClient;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Application.UseCases;

namespace TransporteDigitalRD.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OportunidadesController : ControllerBase
  {
    OportunidadesService oportunidadesService;
    public OportunidadesController(OportunidadesService service) {
      oportunidadesService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetOportunidadesAsync()
    {
      var response = oportunidadesService.GetOportunidades();
      return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOportunidadAsync(string id)
    {
      var response = oportunidadesService.GetOportunidades(new OportunidadesRequest { id = id});
      return Ok(response);
    }
  }
}
