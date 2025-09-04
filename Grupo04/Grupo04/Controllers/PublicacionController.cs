using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionService _service;
        public PublicacionController(IPublicacionService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/publicacions")]
        public async Task<IEnumerable<PublicacionDtoOut>> Publicacions()
        {
            return await _service.GetPublicacions();
        }

        [HttpGet("api/v1/publicacion/id/{id}")]
        public async Task<ActionResult<PublicacionDtoOut>> GetPublicacionById(int id)
        {
            var publicacion = await _service.GetPublicacionDtoById(id);
            if (publicacion is null)
            {
                return NotFound(id);
            }
            else
            {
                return publicacion;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/publicacion")]
        public async Task<IActionResult> Create(PublicacionDtoIn publicacionDtoIn)
        {
            var newPublicacion = await _service.Create(publicacionDtoIn);

            return CreatedAtAction(nameof(GetPublicacionById), new { id = newPublicacion.Id }, newPublicacion);
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, PublicacionDtoIn publicacionDtoIn)
        {
            if (id != publicacionDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({publicacionDtoIn.Id}) del cuerpo de la solicitud" });

            var publicacionToUpdate = await _service.GetPublicacionDtoById(id);

            if (publicacionToUpdate is not null)
            {
                await _service.Update(id, publicacionDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/publicacion/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetPublicacionDtoById(id);

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
