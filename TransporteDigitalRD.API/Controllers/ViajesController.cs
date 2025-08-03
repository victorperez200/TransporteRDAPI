using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Application.UseCases;

namespace TransporteDigitalRD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajesController : ControllerBase
    {
        private readonly ViajesService _viajeService;

        public ViajesController(ViajesService viajesService)
        {
            _viajeService = viajesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViajes()
        {
            var viajeList = _viajeService.GetViajes();
            return Ok(viajeList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateViaje(CreateViajeDto dto)
        {
            return Ok(_viajeService.CreateViaje(dto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteViaje(int id)
        {
            _viajeService.DeleteViaje(id);
            return NoContent();
        }
    }
}
