using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajeController : ControllerBase
    {
        private readonly IMensajeService _service;
        public MensajeController(IMensajeService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/mensajes")]
        public async Task<IEnumerable<MensajeDtoOut>> Mensajes()
        {
            return await _service.GetMensajes();
        }

        [HttpGet("api/v1/mensaje/id/{id}")]
        public async Task<ActionResult<MensajeDtoOut>> GetMensajeById(int id)
        {
            var mensaje = await _service.GetMensajeDtoById(id);
            if (mensaje is null)
            {
                return NotFound(id);
            }
            else
            {
                return mensaje;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/mensaje")]
        public async Task<IActionResult> Create(MensajeDtoIn mensajeDtoIn)
        {
            var newMensaje = await _service.Create(mensajeDtoIn);

            return CreatedAtAction(nameof(GetMensajeById), new { id = newMensaje.Id }, newMensaje);
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, MensajeDtoIn mensajeDtoIn)
        {
            if (id != mensajeDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({mensajeDtoIn.Id}) del cuerpo de la solicitud" });

            var mensajeToUpdate = await _service.GetMensajeDtoById(id);

            if (mensajeToUpdate is not null)
            {
                await _service.Update(id, mensajeDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/mensaje/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetMensajeDtoById(id);

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
