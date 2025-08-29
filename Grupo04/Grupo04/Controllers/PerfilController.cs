using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilService _service;
        public PerfilController(IPerfilService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/perfils")]
        public async Task<IEnumerable<PerfilDtoOut>> Perfils()
        {
            return await _service.GetPerfils();
        }

        [HttpGet("api/v1/perfil/id/{id}")]
        public async Task<ActionResult<PerfilDtoOut>> GetPerfilById(int id)
        {
            var perfil = await _service.GetPerfilDtoById(id);
            if (perfil is null)
            {
                return NotFound(id);
            }
            else
            {
                return perfil;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/perfil")]
        public async Task<IActionResult> Create(PerfilDtoIn perfilDtoIn)
        {
            var newPerfil = await _service.Create(perfilDtoIn);

            return CreatedAtAction(nameof(GetPerfilById), new { id = newPerfil.Id });
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, PerfilDtoIn perfilDtoIn)
        {
            if (id != perfilDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({perfilDtoIn.Id}) del cuerpo de la solicitud" });

            var perfilToUpdate = await _service.GetPerfilDtoById(id);

            if (perfilToUpdate is not null)
            {
                await _service.Update(id, perfilDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/perfil/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetPerfilDtoById(id);

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
