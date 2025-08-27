using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalidadController : ControllerBase
    {
        private readonly ILocalidadService _service;
        public LocalidadController(ILocalidadService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/localidads")]
        public async Task<IEnumerable<LocalidadDtoOut>> Localidads()
        {
            return await _service.GetLocalidads();
        }

        [HttpGet("api/v1/localidad/id/{id}")]
        public async Task<ActionResult<LocalidadDtoOut>> GetLocalidadById(int id)
        {
            var localidad = await _service.GetLocalidadDtoById(id);
            if (localidad is null)
            {
                return NotFound(id);
            }
            else
            {
                return localidad;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/localidad")]
        public async Task<IActionResult> Create(LocalidadDtoIn localidadDtoIn)
        {
            var newLocalidad = await _service.Create(localidadDtoIn);

            return CreatedAtAction(nameof(GetLocalidadById), new { id = newLocalidad.Id });
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, LocalidadDtoIn localidadDtoIn)
        {
            if (id != localidadDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({localidadDtoIn.Id}) del cuerpo de la solicitud" });

            var localidadToUpdate = await _service.GetLocalidadDtoById(id);

            if (localidadToUpdate is not null)
            {
                await _service.Update(id, localidadDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/localidad/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetLocalidadDtoById(id);

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
