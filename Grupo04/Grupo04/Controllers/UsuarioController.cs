using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
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
        public async Task<ActionResult<IEnumerable<UsuarioDtoOut>>> Usuarios()
        {
            var usuarios = await _service.GetUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("api/v1/usuario/id/{id:int}")]
        public async Task<ActionResult<UsuarioDtoOut>> GetUsuarioById(int id)
        {
            var usuario = await _service.GetUsuarioDtoById(id);
            if (usuario is null)
                return NotFound(new { mensaje = $"No se encontró un usuario con ID = {id}" });

            return Ok(usuario);
        }

        //agregar
        [HttpPost("api/v1/agrega/usuario")]
        public async Task<ActionResult<UsuarioAuthResponse>> Create([FromBody]  UsuarioDtoIn usuarioDtoIn)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _service.Create(usuarioDtoIn);

            if (!resultado.EsCorrecto)
                return Conflict(new { mensaje = $"El email '{usuarioDtoIn.Email}' ya está registrado." });

            return CreatedAtAction(nameof(GetUsuarioById), new { id = resultado.Id }, resultado);
        }

        // ✅ Login (valida credenciales y devuelve token)
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioAuthResponse>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _service.Login(request.Email, request.Password);

            if (!resultado.EsCorrecto)
                return Unauthorized(new { mensaje = "Email o contraseña incorrectos." });

            return Ok(resultado);
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDtoIn usuarioDtoIn)
        {
            if (id != usuarioDtoIn.Id)
                return BadRequest(new { mensaje = $"El ID ({id}) de la URL no coincide con el ID ({usuarioDtoIn.Id}) del cuerpo de la solicitud." });

            var usuarioExistente = await _service.GetUsuarioDtoById(id);
            if (usuarioExistente is null)
                return NotFound(new { mensaje = $"No se encontró un usuario con ID = {id}" });

            await _service.Update(id, usuarioDtoIn);
            return NoContent();
        }


        //ELIMINAr
        [HttpDelete("api/v1/delete/usuario/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _service.GetUsuarioDtoById(id);
            if (usuario is null)
                return NotFound(new { mensaje = $"No se encontró un usuario con ID = {id}" });

            await _service.Delete(id);
            return Ok(new { mensaje = $"Usuario con ID = {id} eliminado correctamente." });
        }
    }
}
