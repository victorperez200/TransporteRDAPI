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

        [HttpGet("{usuarioId}")]
        [HttpGet("historial/{usuarioId}")]
        public async Task<IActionResult> GetHistorialDeViajes(int usuarioId)
        {
            try
            {
                // Llamar al servicio para obtener los viajes terminados y cancelados
                var viajesHistorial = await _viajeService.GetHistorialViajesPorUsuario(usuarioId);

                // Si no se encuentran viajes, devolver un mensaje de no encontrado
                if (viajesHistorial == null || !viajesHistorial.Any())
                {
                    return NotFound(new { Message = "No se encontraron viajes terminados o cancelados para el usuario." });
                }

                // Si se encontraron viajes, devolverlos en una respuesta OK
                return Ok(viajesHistorial);
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver un mensaje de error
                return StatusCode(500, new { Message = "Ocurrió un error al obtener el historial de viajes.", Error = ex.Message });
            }
        }
        [HttpPost("actual/post/")]
        public async Task<IActionResult> PostViajeActual([FromBody] Viaje_Actualrequest viaje_Actualrequest)
        {
            var viaje = _viajeService.PostViajeActual(viaje_Actualrequest);
            if (viaje == null)
            {
                return NotFound();
            }
            return Ok(viaje);
        }
        [HttpGet("actual/Get/{id:int}")]
        public async Task<IActionResult> GetViajeActual(int id)
        {
            var viaje = _viajeService.GetViajeActual(id);
            if (viaje == null)
            {
                return NotFound();
            }
            return Ok(viaje);
        }
        [HttpPut("actual/Put/{id:int}")]
        public async Task<IActionResult> PutViajeActual(int id, [FromBody] Viaje_ActualUpdate dto)
        {
  

            var viaje = _viajeService.PutViajeActual(id,dto);
            if (viaje == null)
            {
                return NotFound();
            }
            return Ok(viaje);
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
