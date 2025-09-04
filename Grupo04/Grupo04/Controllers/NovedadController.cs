using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovedadController : ControllerBase
    {
        private readonly INovedadService _service;
        public NovedadController(INovedadService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/novedades")]
        public async Task<IEnumerable<NovedadDtoOut>> Novedades()
        {
            return await _service.GetNovedads();
        }

        [HttpGet("api/v1/novedad/id/{id}")]
        public async Task<ActionResult<NovedadDtoOut>> GetNovedadById(int id)
        {
            var novedad = await _service.GetNovedadDtoById(id);
            if (novedad is null)
            {
                return NotFound(id);
            }
            else
            {
                return novedad;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/novedad")]
        public async Task<IActionResult> Create(NovedadDtoIn novedadDtoIn)
        {
            var newNovedad = await _service.Create(novedadDtoIn);

            return CreatedAtAction(nameof(GetNovedadById), new { id = newNovedad.Id }, newNovedad);
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, NovedadDtoIn novedadDtoIn)
        {
            if (id != novedadDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({novedadDtoIn.Id}) del cuerpo de la solicitud" });

            var novedadToUpdate = await _service.GetNovedadDtoById(id);

            if (novedadToUpdate is not null)
            {
                await _service.Update(id, novedadDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/novedad/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetNovedadDtoById(id);

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
