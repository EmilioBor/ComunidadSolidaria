using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioReporteController : ControllerBase
    {
        private readonly IUsuarioReporte _service;
        public UsuarioReporteController(IUsuarioReporte service)
        {
            _service = service;
        }

        // GET: api/UsuarioReporte/api/v1/usuarioReportes
        [HttpGet("api/v1/usuarioReportes")]
        public async Task<IEnumerable<UsuarioReporteDtoOut>> GetUsuarioReportes()
        {
            return await _service.GetUsuarioReportes();
        }

        // GET: api/UsuarioReporte/api/v1/usuarioReporte/id/5
        [HttpGet("api/v1/usuarioReporte/id/{id}")]
        public async Task<ActionResult<UsuarioReporteDtoOut>> GetUsuarioReporteById(int id)
        {
            var usuarioReporte = await _service.GetUsuarioReporteDtoById(id);
            if (usuarioReporte == null)
            {
                return NotFound(new { message = $"Reporte con ID {id} no encontrado" });
            }
            return usuarioReporte;
        }

        // POST: api/UsuarioReporte/api/v1/agrega/usuarioReporte
        // 🔹 Devuelve el reporte creado + total reportes del perfil
        [HttpPost("api/v1/agrega/usuarioReporte")]
        public async Task<IActionResult> Create(UsuarioReporteDtoIn usuarioReporteDtoIn)
        {
            try
            {
                var (reporteCreado, totalReportesPerfil) = await _service.CreateConContador(usuarioReporteDtoIn);

                return CreatedAtAction(
                    nameof(GetUsuarioReporteById),
                    new { id = reporteCreado.Id },
                    new
                    {
                        reporte = reporteCreado,
                        totalReportesPerfil = totalReportesPerfil
                    }
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/UsuarioReporte/api/v1/editar/5
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, UsuarioReporteDtoIn usuarioReporteDtoIn)
        {
            if (id != usuarioReporteDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({usuarioReporteDtoIn.Id}) del cuerpo de la solicitud" });

            var usuarioReporteToUpdate = await _service.GetUsuarioReporteDtoById(id);
            if (usuarioReporteToUpdate != null)
            {
                await _service.Update(id, usuarioReporteDtoIn);
                return NoContent();
            }
            return NotFound(new { message = $"Reporte con ID {id} no encontrado" });
        }

        // DELETE: api/UsuarioReporte/api/v1/delete/usuarioReporte/5
        [HttpDelete("api/v1/delete/usuarioReporte/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetUsuarioReporteDtoById(id);
            if (toDelete != null)
            {
                await _service.Delete(id);
                return Ok(new { message = $"Reporte con ID {id} eliminado correctamente" });
            }
            return NotFound(new { message = $"Reporte con ID {id} no encontrado" });
        }
    }
}
