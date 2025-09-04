using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacionService _service;
        public NotificacionController(INotificacionService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/notificacions")]
        public async Task<IEnumerable<NotificacionDtoOut>> Notificacions()
        {
            return await _service.GetNotificacions();
        }

        [HttpGet("api/v1/notificacion/id/{id}")]
        public async Task<ActionResult<NotificacionDtoOut>> GetNotificacionById(int id)
        {
            var notificacion = await _service.GetNotificacionDtoById(id);
            if (notificacion is null)
            {
                return NotFound(id);
            }
            else
            {
                return notificacion;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/notificacion")]
        public async Task<IActionResult> Create(NotificacionDtoIn notificacionDtoIn)
        {
            var newNotificacion = await _service.Create(notificacionDtoIn);

            return CreatedAtAction(nameof(GetNotificacionById), new { id = newNotificacion.Id }, newNotificacion);
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, NotificacionDtoIn notificacionDtoIn)
        {
            if (id != notificacionDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({notificacionDtoIn.Id}) del cuerpo de la solicitud" });

            var notificacionToUpdate = await _service.GetNotificacionDtoById(id);

            if (notificacionToUpdate is not null)
            {
                await _service.Update(id, notificacionDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/notificacion/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetNotificacionDtoById(id);

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
