using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/usuarios")]
        public async Task<IEnumerable<UsuarioDtoOut>> Usuarios()
        {
            return await _service.GetUsuarios();
        }

        [HttpGet("api/v1/usuario/id/{id}")]
        public async Task<ActionResult<UsuarioDtoOut>> GetUsuarioById(int id)
        {
            var usuario = await _service.GetUsuarioDtoById(id);
            if (usuario is null)
            {
                return NotFound(id);
            }
            else
            {
                return usuario;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/usuario")]
        public async Task<IActionResult> Create(UsuarioDtoIn usuarioDtoIn)
        {
            var newUsuario = await _service.Create(usuarioDtoIn);

            return CreatedAtAction(nameof(GetUsuarioById), new { id = newUsuario.Id }, newUsuario);
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, UsuarioDtoIn usuarioDtoIn)
        {
            if (id != usuarioDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({usuarioDtoIn.Id}) del cuerpo de la solicitud" });

            var usuarioToUpdate = await _service.GetUsuarioDtoById(id);

            if (usuarioToUpdate is not null)
            {
                await _service.Update(id, usuarioDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/usuario/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetUsuarioDtoById(id);

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
