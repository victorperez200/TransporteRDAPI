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
    public class ReportarProblemaController : ControllerBase
    {
        private readonly ReportarProblemaService _service;
        public ReportarProblemaController(ReportarProblemaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetReportarProblema()
        {
            var reportarProblemaList = _service.GetReportarProblema();
            return Ok(reportarProblemaList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReportarProblema([FromBody] CreateReportarProblemaDto dto)
        {
            await _service.CreateReportarProblema(dto);
            return Created();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReportarProblema(int id)
        {
            await _service.DeleteReportarProblema(id);
            return NoContent();
        }
    }
}
