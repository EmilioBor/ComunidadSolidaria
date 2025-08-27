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
        private readonly IDetalleDonacionService _service;
        public DetalleDonacionController(IDetalleDonacionService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/detalleDonacions")]
        public async Task<IEnumerable<DetalleDonacionDtoOut>> DetalleDonacions()
        {
            return await _service.GetDetalleDonacions();
        }

        [HttpGet("api/v1/detalleDonacion/id/{id}")]
        public async Task<ActionResult<DetalleDonacionDtoOut>> GetDetalleDonacionById(int id)
        {
            var detalleDonacion = await _service.GetDetalleDonacionDtoById(id);
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
        public async Task<IActionResult> Create(DetalleDonacionDtoIn detalleDonacionDtoIn)
        {
            var newDetalleDonacion = await _service.Create(detalleDonacionDtoIn);

            return CreatedAtAction(nameof(GetDetalleDonacionById), new { id = newDetalleDonacion.Id });
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, DetalleDonacionDtoIn detalleDonacionDtoIn)
        {
            if (id != detalleDonacionDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({detalleDonacionDtoIn.Id}) del cuerpo de la solicitud" });

            var detalleDonacionToUpdate = await _service.GetDetalleDonacionDtoById(id);

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
            var toDelete = await _service.GetDetalleDonacionDtoById(id);

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
