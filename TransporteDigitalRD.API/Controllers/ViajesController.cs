using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransporteDigitalRD.Application.DTOs;
using TransporteDigitalRD.Application.UseCases;

namespace TransporteDigitalRD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> CreateViaje([FromBody] CreateViajeDto dto)
        {
            return Ok(_viajeService.CreateViaje(dto));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateViaje(int id, [FromBody] UpdateViajeDto dto)
        {
            var updatedViaje = _viajeService.UpdateViaje(id, dto);
            return Ok(updatedViaje);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteViaje(int id)
        {
            _viajeService.DeleteViaje(id);
            return NoContent();
        }
    }
}
