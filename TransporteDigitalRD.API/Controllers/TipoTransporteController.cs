using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransporteDigitalRD.Application.UseCases;

namespace TransporteDigitalRD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[Authorize]
    public class TipoTransporteController : ControllerBase
    {
        private readonly TipoTransporteService _transporteService;
        public TipoTransporteController(TipoTransporteService tipoTransporteService)
        {
            _transporteService = tipoTransporteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_transporteService.GetTipoTransporte());
        }
    }
}
