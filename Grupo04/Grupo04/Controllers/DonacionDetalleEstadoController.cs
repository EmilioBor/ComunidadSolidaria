using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DetalleDonacionController : ControllerBase
    {
        private readonly IDonacionDetalleEstadoService _service;
        public DetalleDonacionController(IDonacionDetalleEstadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<DonacionDetalleEstadoDtoOut>> GetAll()
        {
            return await _service.GetDonacionDetalleEstados();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonacionDetalleEstadoDtoOut>> GetDetalleDonacionById(int id)
        {
            var detalleDonacion = await _service.GetDonacionDetalleEstadoDtoById(id);
            if (detalleDonacion is null)
                return NotFound(id);

            return detalleDonacion;
        }

        [HttpGet("ultimo/{descripcion}")]
        public async Task<ActionResult<DonacionDetalleEstadoDtoOut>> GetDonacionDetalleUltimo(string descripcion)
        {
            var detalleDonacion = await _service.GetDonacionDetalleEstadoUltimo(descripcion);
            if (detalleDonacion is null)
                return NotFound(descripcion);

            return detalleDonacion;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DonacionDetalleEstadoDtoIn dto)
        {
            var nuevo = await _service.Create(dto);

            return CreatedAtAction(
                nameof(GetDetalleDonacionById),
                new { id = nuevo.Id },
                nuevo
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DonacionDetalleEstadoDtoIn dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID no coincide.");

            var existente = await _service.GetDonacionDetalleEstadoDtoById(id);

            if (existente is null)
                return NotFound(id);

            await _service.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetDonacionDetalleEstadoDtoById(id);

            if (toDelete is null)
                return NotFound(id);

            await _service.Delete(id);
            return Ok();
        }
    }

}
