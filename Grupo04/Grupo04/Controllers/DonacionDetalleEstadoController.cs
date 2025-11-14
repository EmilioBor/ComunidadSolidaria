using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleDonacionController : ControllerBase
    {
        private readonly IDonacionDetalleEstadoService _service;
        public DetalleDonacionController(IDonacionDetalleEstadoService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/detalleDonacions")]
        public async Task<IEnumerable<DonacionDetalleEstadoDtoOut>> DetalleDonacions()
        {
            return await _service.GetDonacionDetalleEstados();
        }

        [HttpGet("api/v1/detalleDonacion/id/{id}")]
        public async Task<ActionResult<DonacionDetalleEstadoDtoOut>> GetDetalleDonacionById(int id)
        {
            var detalleDonacion = await _service.GetDonacionDetalleEstadoDtoById(id);
            if (detalleDonacion is null)
            {
                return NotFound(id);
            }
            else
            {
                return detalleDonacion;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/detalleDonacion")]
        public async Task<IActionResult> Create(DonacionDetalleEstadoDtoIn detalleDonacionDtoIn)
        {
            var newDetalleDonacion = await _service.Create(detalleDonacionDtoIn);

            return CreatedAtAction(nameof(GetDetalleDonacionById), new { id = newDetalleDonacion.Id });
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, DonacionDetalleEstadoDtoIn detalleDonacionDtoIn)
        {
            if (id != detalleDonacionDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({detalleDonacionDtoIn.Id}) del cuerpo de la solicitud" });

            var detalleDonacionToUpdate = await _service.GetDonacionDetalleEstadoDtoById(id);

            if (detalleDonacionToUpdate is not null)
            {
                await _service.Update(id, detalleDonacionDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/detalleDonacion/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetDonacionDetalleEstadoDtoById(id);

            if (toDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
